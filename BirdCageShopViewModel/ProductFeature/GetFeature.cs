using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.ProductFeature
{
	public class GetFeature
	{
        public string FeatureName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModiedAt { get; set; }
        public decimal? Price { get; set; }
    }
}
