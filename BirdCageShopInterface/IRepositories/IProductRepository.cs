using BirdCageShopDbContext.Models;
using BirdCageShopUtils.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IRepositories
{
	public interface IProductRepository : IBaseRepository<Product>
	{


		Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> GetProductForDesign();
		Task<Product?> GetProductEmptyByIdAsync(int id);
		Task<Pagination<Product>> GetPaginationAllProductAsync(int pageIndex, int pageSize);
        Task<Product> GetProductWithReviewByProIdAsync(int productID);
		Task<Product?> GetProductByProductIdAndCustomerIdAsync(string customerId, int productId);

		Task<Product> GetProductIncludeImage(int productID);
		Task<IEnumerable<Product>> GetProductsFromWishlistAsync(string customerId);

        Task<Product> GetByIdProductDeletedAsync(int productID);
        Task<Product?> FirstOrDefaultAsync();



    }
}
