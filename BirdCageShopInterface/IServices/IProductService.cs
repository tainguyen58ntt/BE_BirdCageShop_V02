﻿using BirdCageShopDbContext.Models;
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
		Task<ProductViewModel?> GetByIdAsync(int id);
		Task<Product?> GetProductByIdAsync(int id);
		Task<IEnumerable<ProductViewModel>> GetProductByCategoryAsync(int categoryId);
		Task<bool> RemoveAsync(Product product);
	}
}