using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class ProductSpecification
    {
        public int Id { get; set; }
        public string SpecificationName { get; set; } = null!;
        public string SpecificationValue { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModiedAt { get; set; }
        public decimal? Price { get; set; }
        public int? ProductId { get; set; }
    }
}
