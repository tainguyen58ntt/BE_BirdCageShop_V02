using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
	public class ShoppingCartRepository : BaseRepository<ShoppingCart>, IShoppingCartRepository
	{
		public ShoppingCartRepository(BirdCageShopContext context) : base(context)
		{
		}

		public async Task<IEnumerable<ShoppingCart>> GetShoppingCartsAsync(int customerId)
		{
			return await _context.Set<ShoppingCart>()
			  .AsNoTracking()
			  .Include(p => p.Product)
			  //.Where(x => x.ExpDate >= DateTime.Now)
			  .ToListAsync();
		}
	}
}
