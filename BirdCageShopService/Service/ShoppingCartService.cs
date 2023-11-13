using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopUtils;
using BirdCageShopViewModel.BirdCageType;
using BirdCageShopViewModel.Design;
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
        private readonly IConfirmOrderValidator _confirmOrderValidator;
        private IDesignService _designService;
        private IBirdCageTypeService _birdCageTypeService;


        public ShoppingCartService(IBirdCageTypeService birdCageTypeService, IDesignService designService, IConfirmOrderValidator confirmOrderValidator, IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {
            _confirmOrderValidator = confirmOrderValidator;
            _designService = designService;
            _birdCageTypeService = birdCageTypeService;
        }


        /*        public async Task<bool> CheckoutAsync(ConfirmOrderAddViewModel confirmOrderAddViewModel)
                {
                    bool isSuccess = false;
                    Voucher vourcher = new Voucher();
                    var currentUserId = _claimService.GetCurrentUserId();
                    if (currentUserId == null) return false;


                    // get status
                    //string? orderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1);
                    //string? paymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1);

                    //if (orderStatus == null && paymentStatus == null)
                    //{
                    //    return false;
                    //}

                    // get 
                    List<ShoppingCart> shoppingCarts = (List<ShoppingCart>)await _unitOfWork.ShoppingCartRepository.GetShoppingCartsAsync(currentUserId);
                    decimal? totalPrice = 0;
                    decimal? beforeVoucherPrice = 0;
                    foreach (var x in shoppingCarts)
                    {
                        beforeVoucherPrice += (x.Product.PriceAfterDiscount * x.Count);
                    }
                    //
                    if (confirmOrderAddViewModel.VourcherCode != null)
                    {
                        // get discount of voucher and update Total
                        vourcher = await _unitOfWork.VoucherRepository.GetVoucherByCodeAsync(confirmOrderAddViewModel.VourcherCode);
                        totalPrice = beforeVoucherPrice - beforeVoucherPrice * vourcher.DiscountPercent;
                    }
                    else
                    {

                        totalPrice = beforeVoucherPrice;
                    }
                    //
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

                    Order order = new Order();
                    if (confirmOrderAddViewModel.PaymentMethod.Equals(PaymentMethod.COD))
                    {
                        if (confirmOrderAddViewModel.VourcherCode != null)
                        {
                            order = new Order
                            {
                                ApplicationUserId = currentUserId,
                                NameRecieved = confirmOrderAddViewModel.Name,
                                OrderDate = _timeService.GetCurrentTimeInVietnam(),
                                TotalPriceBeforeVoucher = beforeVoucherPrice,
                                TotalPrice = totalPrice,
                                OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1),  // pending
                                PhoneNumber = confirmOrderAddViewModel.Phone,
                                StreetAddress = confirmOrderAddViewModel.StreetAddress,
                                City = confirmOrderAddViewModel.City,
                                PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(6),  // cod
                                //VoucherId = vourcher.Id,
                                VoucherCode = confirmOrderAddViewModel.VourcherCode,
                                Details = orderDetail
                            };
                        }
                        else
                        {

                            order = new Order
                            {
                                ApplicationUserId = currentUserId,
                                NameRecieved = confirmOrderAddViewModel.Name,
                                OrderDate = _timeService.GetCurrentTimeInVietnam(),
                                TotalPrice = totalPrice,
                                OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1),  // pending
                                PhoneNumber = confirmOrderAddViewModel.Phone,
                                StreetAddress = confirmOrderAddViewModel.StreetAddress,
                                City = confirmOrderAddViewModel.City,
                                PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(6),   // cod
                                Details = orderDetail


                            };
                        }
                    }
                    else if (confirmOrderAddViewModel.PaymentMethod.Equals(PaymentMethod.PAYONLINE))
                    {
                        if (confirmOrderAddViewModel.VourcherCode != null)
                        {
                            order = new Order
                            {
                                ApplicationUserId = currentUserId,
                                NameRecieved = confirmOrderAddViewModel.Name,
                                OrderDate = _timeService.GetCurrentTimeInVietnam(),
                                TotalPrice = totalPrice,
                                OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(2),  // approved,
                                PhoneNumber = confirmOrderAddViewModel.Phone,
                                StreetAddress = confirmOrderAddViewModel.StreetAddress,
                                City = confirmOrderAddViewModel.City,
                                PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(7),   // PAYONLINE,
                                //VoucherId = vourcher.Id,
                                Details = orderDetail
                            };
                        }
                        else
                        {

                            order = new Order
                            {
                                ApplicationUserId = currentUserId,
                                NameRecieved = confirmOrderAddViewModel.Name,
                                OrderDate = _timeService.GetCurrentTimeInVietnam(),
                                TotalPrice = totalPrice,
                                OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(2),  // approved,,
                                PhoneNumber = confirmOrderAddViewModel.Phone,
                                StreetAddress = confirmOrderAddViewModel.StreetAddress,
                                City = confirmOrderAddViewModel.City,
                                PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(7),   // PAYONLINE,,
                                Details = orderDetail


                            };
                        }


                    }



                    await _unitOfWork.OrderRepository.AddAsync(order);



                    //+delete shopping cart
                    await _unitOfWork.ShoppingCartRepository.DeleteShoppingCartsByUserIdAsync(currentUserId);

                    return await _unitOfWork.SaveChangesAsync();
                }
        */
        public async Task<bool> CheckoutAsync(ConfirmOrderAddViewModel confirmOrderAddViewModel)
        {

            Voucher vourcher = new Voucher();
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;
            List<ShoppingCart> shoppingCarts = (List<ShoppingCart>)await _unitOfWork.ShoppingCartRepository.GetShoppingCartsAsync(currentUserId);
            decimal? totalPrice = 0;
            decimal? beforeVoucherPrice = 0;
            foreach (var x in shoppingCarts)
            {
                if (x.Product.PercentDiscount == null || x.Product.PercentDiscount == 0)
                {
                    beforeVoucherPrice += x.Product.Price * x.Count;
                }
                else
                {

                    beforeVoucherPrice += (x.Product.PriceAfterDiscount * x.Count);
                }
            }
            //
            if (confirmOrderAddViewModel.VourcherCode != null)
            {
                // get discount of voucher and update Total
                vourcher = await _unitOfWork.VoucherRepository.GetVoucherByCodeAsync(confirmOrderAddViewModel.VourcherCode);
                totalPrice = beforeVoucherPrice - beforeVoucherPrice * vourcher.DiscountPercent;
            }
            else
            {

                totalPrice = beforeVoucherPrice;
            }
            //
            //+update to orderdetail
            List<OrderDetail> orderDetail = new List<OrderDetail>();
            foreach (var x in shoppingCarts)
            {
                if (x.PriceDesign == null)
                {
                    if(x.Product.PercentDiscount == null || x.Product.PercentDiscount == 0)
                    {
                        OrderDetail _orderDetail = new OrderDetail()
                        {
                            ProductId = x.ProductId,
                            Quantity = x.Count,

                            //Price = (x.Product.Price * x.Count)
                            Price = x.Product.Price
                        };
                        //totalPrice = totalPrice + _orderDetail.Price;
                        orderDetail.Add(_orderDetail);
                    }
                    else
                    {
                        OrderDetail _orderDetail = new OrderDetail()
                        {
                            ProductId = x.ProductId,
                            Quantity = x.Count,

                            //Price = (x.Product.PriceAfterDiscount * x.Count)
                            Price = x.Product.PriceAfterDiscount
                        };
                        //totalPrice = totalPrice + _orderDetail.Price;
                        orderDetail.Add(_orderDetail);
                    }
                
                    //await _unitOfWork.OrderDetailRepository.AddAsync(orderDetail);
                }
                else
                {
                    OrderDetail _orderDetail = new OrderDetail()
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Count,
                        Price = (x.Product.PriceAfterDiscount + x.PriceDesign),
                        Model = x.Model,
                        Width = x.Width,
                        Height = x.Height,
                        Material = x.Material,
                        Bars = x.Bars,
                    };
                    //totalPrice = totalPrice + _orderDetail.Price;
                    orderDetail.Add(_orderDetail);
                }
            }

            // order detail

            /// update quatity in stock of product 
            /// 

            /// update quatity in stock of product 

            Order order = new Order();
            if (confirmOrderAddViewModel.PaymentMethod.Equals(PaymentMethod.COD))
            {
                if (confirmOrderAddViewModel.VourcherCode != null)
                {
                    order = new Order
                    {
                        ApplicationUserId = currentUserId,
                        NameRecieved = confirmOrderAddViewModel.Name,
                        OrderDate = _timeService.GetCurrentTimeInVietnam(),
                        TotalPriceBeforeVoucher = beforeVoucherPrice,
                        TotalPrice = totalPrice,
                        OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1),  // pending
                        PhoneNumber = confirmOrderAddViewModel.Phone,
                        StreetAddress = confirmOrderAddViewModel.StreetAddress,
                        City = confirmOrderAddViewModel.City,
                        PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(6),  // cod
                        //VoucherId = vourcher.Id,
                        VoucherCode = confirmOrderAddViewModel.VourcherCode,
                        Details = orderDetail,
                        ShipCost = 30000
                        
                    };
                }
                else
                {

                    order = new Order
                    {
                        ApplicationUserId = currentUserId,
                        NameRecieved = confirmOrderAddViewModel.Name,
                        OrderDate = _timeService.GetCurrentTimeInVietnam(),
                        TotalPrice = totalPrice,
                        OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1),  // pending
                        PhoneNumber = confirmOrderAddViewModel.Phone,
                        StreetAddress = confirmOrderAddViewModel.StreetAddress,
                        City = confirmOrderAddViewModel.City,
                        PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(6),   // cod
                        Details = orderDetail,
                        ShipCost = 30000

                    };
                }
            }
            else if (confirmOrderAddViewModel.PaymentMethod.Equals(PaymentMethod.PAYONLINE))
            {
                if (confirmOrderAddViewModel.VourcherCode != null)
                {
                    order = new Order
                    {
                        ApplicationUserId = currentUserId,
                        NameRecieved = confirmOrderAddViewModel.Name,
                        OrderDate = _timeService.GetCurrentTimeInVietnam(),
                        TotalPrice = totalPrice,
                        OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(2),  // approved,
                        PhoneNumber = confirmOrderAddViewModel.Phone,
                        StreetAddress = confirmOrderAddViewModel.StreetAddress,
                        City = confirmOrderAddViewModel.City,
                        PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(7),   // PAYONLINE,
                        //VoucherId = vourcher.Id,
                        Details = orderDetail,
                        ShipCost = 30000
                    };
                }
                else
                {

                    order = new Order
                    {
                        ApplicationUserId = currentUserId,
                        NameRecieved = confirmOrderAddViewModel.Name,
                        OrderDate = _timeService.GetCurrentTimeInVietnam(),
                        TotalPrice = totalPrice,
                        OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(2),  // approved,,
                        PhoneNumber = confirmOrderAddViewModel.Phone,
                        StreetAddress = confirmOrderAddViewModel.StreetAddress,
                        City = confirmOrderAddViewModel.City,
                        PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(7),   // PAYONLINE,,
                        Details = orderDetail,
                        ShipCost = 30000


                    };
                }
            }
            foreach (var o in order.Details)
            {
                var product = await _unitOfWork.ProductRepository.GetProductEmptyByIdAsync(o.ProductId);
                product.QuantityInStock = product.QuantityInStock - orderDetail.Count;
                _unitOfWork.ProductRepository.Update(product);
            }
            await _unitOfWork.OrderRepository.AddAsync(order);



            //+delete shopping cart
            await _unitOfWork.ShoppingCartRepository.DeleteShoppingCartsByUserIdAsync(currentUserId);
            await _designService.DeleteDesignAsync(currentUserId);
            return await _unitOfWork.SaveChangesAsync();
         }




        public async Task<bool> CreateOrUpdateAsync(int productId, int count)
        {
            string currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;
            var product = await _unitOfWork.ProductRepository.GetProductEmptyByIdAsync(productId);
            if (product.QuantityInStock < 1) return false;
            var cartItem = await _unitOfWork.ShoppingCartRepository.GetCartItemByUserIdAndProDIdAsync(currentUserId, productId);
            if (cartItem == null && product.isEmpty == false)
            {
                cartItem = new ShoppingCart
                {
                    ProductId = productId,
                    Count = count,
                    ApplicationUserId = currentUserId,
                    CreatedAt = _timeService.GetCurrentTimeInVietnam()
                };
                if (count > product.QuantityInStock) return false;
                await _unitOfWork.ShoppingCartRepository.AddAsync(cartItem);
                return await _unitOfWork.SaveChangesAsync();
            }
            if (cartItem == null && product.isEmpty == true)
            {
                List<DesignViewModel> designs = await _designService.GetByIdAsync(currentUserId);
                foreach (DesignViewModel design in designs)
                {
                    GetBirdCageType birdType = await _birdCageTypeService.GetAsync(design.BirdCageTypeId);
                    var birdTypeName = birdType.TypeName;
                    cartItem = new ShoppingCart
                    {
                        ProductId = productId,
                        Count = count,
                        ApplicationUserId = currentUserId,
                        CreatedAt = _timeService.GetCurrentTimeInVietnam(),
                        Model = design.Model,
                        Width = design.Width,
                        Height = design.Height,
                        Material = design.Material,
                        Bars = design.Bars,
                        PriceDesign = design.PriceDesign,
                        TypeName = birdTypeName,
                    };
                    if (count > product.QuantityInStock) return false;
                    await _unitOfWork.ShoppingCartRepository.AddAsync(cartItem);
                    return await _unitOfWork.SaveChangesAsync();
                }
            }
            cartItem.Count += count;
            cartItem.ModifiedAt = _timeService.GetCurrentTimeInVietnam();
            _unitOfWork.ShoppingCartRepository.Update(cartItem);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ProductViewModel> ExistProductAsync(int productId)
        {
            var result = await _unitOfWork.ProductRepository.GetProductEmptyByIdAsync(productId);
            return _mapper.Map<ProductViewModel>(result);
        }

        public async Task<bool> ExistProductByIdAndUserIdAsync(int productId)
        {
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;

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


        public async Task<ShoppingCartViewModel?> GetShoppingCartByProIdAsync(int proId)
        {
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return null;

            var result = await _unitOfWork.ShoppingCartRepository.GetCartItemByUserIdAndProDIdAsync(currentUserId, proId);

            return _mapper.Map<ShoppingCartViewModel>(result);
        }

        public async Task<IEnumerable<ShoppingCartViewModel>> GetShoppingCartsAsync()
        {
            //

            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return new List<ShoppingCartViewModel>();


            var result = await _unitOfWork.ShoppingCartRepository.GetShoppingCartsAsync(currentUserId);

            return _mapper.Map<List<ShoppingCartViewModel>>(result);
            //return shoppingCartViewModels;
        }

        public async Task<bool> RemoveFromCartAsync(int productId)
        {
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;

            //

            var cartItem = await _unitOfWork.ShoppingCartRepository.GetCartItemByUserIdAndProDIdAsync(currentUserId, productId);
            if (cartItem is null) return false;
            _unitOfWork.ShoppingCartRepository.Delete(cartItem);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int productId, int count)
        {
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;
            var cartItem = await _unitOfWork.ShoppingCartRepository.GetCartItemByUserIdAndProDIdAsync(currentUserId, productId);
            if (cartItem == null)
            {
                //cartItem = new ShoppingCart
                //{
                //    ProductId = productId,
                //    Count = count,
                //    ApplicationUserId = currentUserId,
                //    CreatedAt = _timeService.GetCurrentTimeInVietnam()
                //};
                //_unitOfWork.ShoppingCartRepository.AddAsync(cartItem);
                //return await _unitOfWork.SaveChangesAsync();
                return false;


            }
            cartItem.Count = count;
            cartItem.ModifiedAt = _timeService.GetCurrentTimeInVietnam();
            _unitOfWork.ShoppingCartRepository.Update(cartItem);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ValidationResult> ValidateConfirmOrderAddAsync(ConfirmOrderAddViewModel vm)
        {
            return await _confirmOrderValidator.ConfirmOrderAddValidator.ValidateAsync(vm);
        }
    }
}
