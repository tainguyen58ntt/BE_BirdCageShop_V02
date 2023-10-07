using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
	public interface IProductService : IBaseService
	{
		Task<IEnumerable<ProductViewModel>> GetProductsAsync();
		Task<Pagination<ProductViewModel>> GetPageAsync(int pageIndex, int pageSize);
	}
}
