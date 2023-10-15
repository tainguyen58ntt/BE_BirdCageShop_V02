using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.ShoppingCart;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BirdCageShopService.Service
{
    public class ShoppingCartService : BaseService, IShoppingCartService
    {
        private readonly IShippingDetailValidator _shippingDetailValidator;


        public ShoppingCartService(IShippingDetailValidator shippingDetailValidator, IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {
            _shippingDetailValidator = shippingDetailValidator;
        }


        public async Task<bool> CheckoutAsync(ShippingDetailAddViewModel shippingDetailAddViewModel)
        {
            bool isSuccess = false;
            Voucher vourcher = new Voucher();
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == -1) return false;

      
            // get status
            string? orderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1);
            string? paymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1);

            if (orderStatus == null && paymentStatus == null)
            {
                return false;
            }

            // get 
            List<ShoppingCart> shoppingCarts = (List<ShoppingCart>)await _unitOfWork.ShoppingCartRepository.GetShoppingCartsAsync(currentUserId);
            decimal? totalPrice = 0;
            foreach (var x in shoppingCarts)
            {
                totalPrice += (x.Product.PriceAfterDiscount * x.Count);
            }
            //
            if (shippingDetailAddViewModel.VourcherCode != null)
            {
                // get discount of voucher and update Total
                vourcher = await _unitOfWork.VoucherRepository.GetVoucherByCodeAsync(shippingDetailAddViewModel.VourcherCode);
                totalPrice = totalPrice - totalPrice * vourcher.DiscountPercent;
            }
            //+update to orderdetail
            List<OrderDetail> orderDetail = new List<OrderDetail>();
            foreach (var x in shoppingCarts)
            {
                OrderDetail _orderDetail = new OrderDetail()
                {
                  
                    ProductId = x.ProductId,
                    Quantity = x.Count,
                    Price = (x.Product.PriceAfterDiscount * x.Count)


                };
                orderDetail.Add(_orderDetail);
                //await _unitOfWork.OrderDetailRepository.AddAsync(orderDetail);

            }

            // order detail

            /// update quatity in stock of product 
            /// 

            /// update quatity in stock of product 

            Order order ;
            if (shippingDetailAddViewModel.VourcherCode != null)
            {
                order = new Order
                {
                    UserId = currentUserId,
                    OrderDate = _timeService.GetCurrentTimeInVietnam(),
                    TotalPrice = totalPrice,
                    OrderStatus = orderStatus,
                    PhoneNumber = shippingDetailAddViewModel.Phone,
                    StreetAddress = shippingDetailAddViewModel.StreetAddress,
                    City = shippingDetailAddViewModel.City,
                    PaymentStatus = paymentStatus,
                    VoucherId = vourcher.Id,
                    Details = orderDetail
                };
            }
            else
            {

                order = new Order
                {
                    UserId = currentUserId,
                    OrderDate = _timeService.GetCurrentTimeInVietnam(),
                    TotalPrice = totalPrice,
                    OrderStatus = orderStatus,
                    PhoneNumber = shippingDetailAddViewModel.Phone,
                    StreetAddress = shippingDetailAddViewModel.StreetAddress,
                    City = shippingDetailAddViewModel.City,
                    PaymentStatus = paymentStatus,
                    Details = orderDetail


                };
            }


             await _unitOfWork.OrderRepository.AddAsync(order);



            //+delete shopping cart
            await _unitOfWork.ShoppingCartRepository.DeleteShoppingCartsByUserIdAsync(currentUserId);   
                
            return await _unitOfWork.SaveChangesAsync();
        }

      


        public async Task<bool> CreateOrUpdateAsync(int productId, int count)
        {
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == -1) return false;
            var cartItem = await _unitOfWork.ShoppingCartRepository.GetCartItemByUserIdAndProDIdAsync(currentUserId, productId);
            if (cartItem == null)
            {
                cartItem = new ShoppingCart
                {
                    ProductId = productId,
                    Count = count,
                    UserId = currentUserId,
                    CreatedAt = _timeService.GetCurrentTimeInVietnam()
                };
                _unitOfWork.ShoppingCartRepository.AddAsync(cartItem);
                return await _unitOfWork.SaveChangesAsync();


            }
            cartItem.Count += count;
            cartItem.ModifiedAt = _timeService.GetCurrentTimeInVietnam();
            _unitOfWork.ShoppingCartRepository.Update(cartItem);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ProductViewModel> ExistProductAsync(int productId)
        {
            var result = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            return _mapper.Map<ProductViewModel>(result);
        }

        public async Task<bool> ExistProductByIdAndUserIdAsync(int productId)
        {
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == -1) return false;

            //

            var cartItem = await _unitOfWork.ShoppingCartRepository.GetCartItemByUserIdAndProDIdAsync(currentUserId, productId);
            if (cartItem is null) return false;
            return true;

        }



        //public async Task<ProductViewModel?> GetProductByIdAsync(int productId)
        //{
        //    var result = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
        //    return _mapper.Map<ProductViewModel>(result);
        //}

        public Task<ShoppingCartViewModel?> GetShoppingCartByIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ShoppingCartViewModel>> GetShoppingCartsAsync()
        {
            //

            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == -1) return new List<ShoppingCartViewModel>();


            var result = await _unitOfWork.ShoppingCartRepository.GetShoppingCartsAsync(currentUserId);

            return _mapper.Map<List<ShoppingCartViewModel>>(result);
            //return shoppingCartViewModels;
        }

        public async Task<bool> RemoveFromCartAsync(int productId)
        {
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == -1) return false;

            //

            var cartItem = await _unitOfWork.ShoppingCartRepository.GetCartItemByUserIdAndProDIdAsync(currentUserId, productId);
            if (cartItem is null) return false;
            _unitOfWork.ShoppingCartRepository.Delete(cartItem);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ValidationResult> ValidateShippingDetailAddAsync(ShippingDetailAddViewModel vm)
        {
            return await _shippingDetailValidator.ShippingDetailAddValidator.ValidateAsync(vm);
        }
    }
}
