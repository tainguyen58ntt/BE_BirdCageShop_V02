using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IRepositories
{
	public interface IShoppingCartRepository : IBaseRepository<ShoppingCart>
	{
		Task<IEnumerable<ShoppingCart>> GetShoppingCartsAsync(int customerId);
        Task DeleteShoppingCartsByUserIdAsync(int customerId);
        Task<ShoppingCart> GetCartItemByUserIdAndProDIdAsync(int customerId, int prodID);
        

    }
}
