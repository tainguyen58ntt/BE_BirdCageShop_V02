using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class ProductFeature
    {
        public int Id { get; set; }
        public string FeatureName { get; set; } = null!;
        public string FeatureValue { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModiedAt { get; set; }
        public decimal? Price { get; set; }
        public int? ProductId { get; set; }
    }
}
