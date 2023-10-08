using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.ProductFeature
{
	public class ProductFeatureViewModel
	{
		public string FeatureName { get; set; } = null!;
		public string FeatureValue { get; set; } = null!;
		public decimal? Price { get; set; }
	}
}
