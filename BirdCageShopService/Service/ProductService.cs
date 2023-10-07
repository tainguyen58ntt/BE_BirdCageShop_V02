using AutoMapper;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.Product;
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

		public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
		{
			var products = await _unitOfWork.ProductRepository.GetAllAsync();
			return _mapper.Map<List<ProductViewModel>>(products);
		}
	}
}
