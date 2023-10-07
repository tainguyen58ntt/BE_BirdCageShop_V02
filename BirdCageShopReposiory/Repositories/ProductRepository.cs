﻿using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
	public class ProductRepository : BaseRepository<Product>, IProductRepository
	{

		public ProductRepository(BirdCageShopContext context) : base(context)
		{
		}

		public override async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await _context.Set<Product>()
				.AsNoTracking()
				.Where(x => !x.isDelete)
				.ToListAsync();
		}

		public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
		{
			return await _context.Set<Product>()
				   .AsNoTracking()
				   .Include(x => x.Category)
				   //.Include(x => x.ProductWishlist)
				   .Where(x => !x.isDelete && x.CategoryId == categoryId)
				   .ToListAsync();
		}
	}
}
