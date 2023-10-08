using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopUtils.Pagination;
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

		public ProductService(ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(timeService, unitOfWork, mapper, configuration)
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

		public async Task<bool> RemoveAsync(Product product)
		{
			product.isDelete = true;
			_unitOfWork.ProductRepository.Update(product);
			return await _unitOfWork.SaveChangesAsync();
		}
	}
}
