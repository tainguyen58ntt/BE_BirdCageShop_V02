using BirdCageShopDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopDomain.Models
{
    public class Design
    {
        public int Id { get; set; }
        public decimal? PriceDesign { get; set; }
        //[NotMapped]
        //public double Price { get; set; }

        public string? Model { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        //public string? Color { get; set; }
        public string? Material { get; set; }
        public int? Bars { get; set; }

        public string? ApplicationUserId { get; set; }

        public int? BirdCageTypeId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        public virtual BirdCageType BirdCageType { get; set; } = null!;

        
    }
}
