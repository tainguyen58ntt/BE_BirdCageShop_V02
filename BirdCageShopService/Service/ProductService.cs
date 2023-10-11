using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Category;
using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.ProductReviews;
using BirdCageShopViewModel.Role;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
	public class ProductService : BaseService, IProductService
	{

		public ProductService(IClaimService claimService,ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
		{
			
		}

		public async Task<ProductViewModel?> GetByIdAsync(int id)
		{
			var result = await _unitOfWork.ProductRepository.GetByIdAsync(id);
			return _mapper.Map<ProductViewModel>(result);
		}

		public async Task<ProductWithReviewViewModel?> GetFeedBackByProductId(int productId)
		{
			
			var result = await _unitOfWork.ProductRepository.GetProductWithReviewByProIdAsync(productId);
			return _mapper.Map<ProductWithReviewViewModel>(result);
		}

		public async Task<Pagination<ProductViewModel>> GetPageAsync(int pageIndex, int pageSize)
		{
			var result = await _unitOfWork.ProductRepository.GetPaginationAsync(pageIndex, pageSize);
			return _mapper.Map<Pagination<ProductViewModel>>(result);
		}

		public async Task<Pagination<ProductViewModel>> GetByBirdCageTypePageAsync(int birdCageTypeId, int pageIndex, int pageSize)
		{

			var result = await _unitOfWork.ProductRepository.GetAllByConditionAsync(c => c.BirdCageTypeId == birdCageTypeId, pageIndex, pageSize);
			return _mapper.Map<Pagination<ProductViewModel>>(result);

			//var result = await _unitOfWork.ProductRepository.GetAllByConditionAsync(pageIndex, pageSize);
			//return _mapper.Map<Pagination<ProductViewModel>>(result);
		}
        public async Task<Pagination<ProductViewModel>> GetByCagegoryTypePageAsync(int categoryId, int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.ProductRepository.GetAllByConditionAsync(c => c.CategoryId == categoryId, pageIndex, pageSize);
            return _mapper.Map<Pagination<ProductViewModel>>(result);
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductByCategoryAsync(int categoryId)
		{
			var result = await _unitOfWork.ProductRepository.GetProductsByCategoryAsync(categoryId);
			return _mapper.Map<List<ProductViewModel>>(result);
		}

		public async Task<Product?> GetProductByIdAsync(int id)
		{
			return await _unitOfWork.ProductRepository.GetByIdAsync(id);
		}

		public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
		{
			var products = await _unitOfWork.ProductRepository.GetAllAsync();
			return _mapper.Map<List<ProductViewModel>>(products);
		}

		public async Task<IEnumerable<ProductFromWishlist>> GetProductsFromWishlistAsync()
		{
			var currentUserId = _claimService.GetCurrentUserId();
			if (currentUserId == -1) return new List<ProductFromWishlist>();
			var result = await _unitOfWork.ProductRepository.GetProductsFromWishlistAsync(currentUserId);
			var productFromWishlistList = new List<ProductFromWishlist>();
			foreach (var product in result)
			{
				var mainImageUrl = GetMainImageUrl(product);
				var productFromWishlist = new ProductFromWishlist
				{
					Title = product.Title,
					Category = new CategoryViewModel
					{
						// Populate CategoryViewModel properties if needed
					},
					PriceAfterDiscount = product.PriceAfterDiscount,
					ImageUrl = mainImageUrl // Set the main image URL
				};
				productFromWishlistList.Add(productFromWishlist);
			}



			return productFromWishlistList;
		}

		private string? GetMainImageUrl(Product product)
		{
			// Find the main product image
			var mainImage = product.ProductImages.FirstOrDefault(pi => pi.IsMainImage && pi.ImageUrl != null);


			if (mainImage != null)
			{
				return mainImage.ImageUrl;
			}
			else
			{
				// Handle case where no main image is found
				return null; // Or return a default image URL
			}
		}

		public async Task<bool> RemoveAsync(Product product)
		{
			product.isDelete = true;
			_unitOfWork.ProductRepository.Update(product);
			return await _unitOfWork.SaveChangesAsync();
		}

      
    }
}
