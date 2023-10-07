﻿using BirdCageShopViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Product
{
	public class ProductViewModel
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public int CategoryId { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? ModifieldAt { get; set; }
		public DateTime? DeletedAt { get; set; }
		//public bool isDelete { get; set; }
		public decimal Price { get; set; }
		public string SKU { get; set; }
		public int QuantityInStock { get; set; }
		public string? EditedBy { get; set; }
		public decimal? PercentDiscount { get; set; }
		public decimal? PriceAfterDiscount { get; set; }

		public CategoryViewModel Category { get; set; }
	}
}
