using BirdCageShopDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Design
{
    public class CreateDesign
    {
        public decimal? PriceDesign { get; set; }
        //[NotMapped]
        //public double Price { get; set; }

        public string? Model { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        //public string? Color { get; set; }
        public string? Material { get; set; }
        public int? Bars { get; set; }

        public string ApplicationUserId { get; set; }

        public int? BirdCageTypeId { get; set; }

    }
}
