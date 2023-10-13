﻿using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface;
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

        //public Task<ShoppingCart> CreateItemByUserIdAndProDIdAsync(int customerId, int prodID)
        //{
        //    var category = _mapper.Map<Category>(vm);
        //    category.CreateAt = DateTime.Now;
        //    await _unitOfWork.CategoryRepository.AddAsync(category);
        //    return await _unitOfWork.SaveChangesAsync();
        //}

        public async Task<ShoppingCart> GetCartItemByUserIdAndProDIdAsync(int customerId, int proDId)
        {
			return await _context.Set<ShoppingCart>()
			  .AsNoTracking()
			  .Include(p => p.Product)
			  .Where(x => x.UserId == customerId && x.ProductId == proDId)
			  .FirstOrDefaultAsync();
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
