using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.ShoppingCart;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class ShoppingCartService : BaseService, IShoppingCartService
    {
        public ShoppingCartService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {
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
                    UserId = currentUserId
                };
                _unitOfWork.ShoppingCartRepository.AddAsync(cartItem);
                return await _unitOfWork.SaveChangesAsync();


            }
            cartItem.Count = count;
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
    }
}
