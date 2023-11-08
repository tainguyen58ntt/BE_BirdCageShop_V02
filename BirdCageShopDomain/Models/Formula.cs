using BirdCageShopDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopDomain.Models
{
    public class Formula
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int MinWidth { get; set; }
        public int MaxWidth { get; set; }
        public int MinHeight { get; set; }
        public int MaxHeight { get; set; }
        //public string Color { get; set; }

        //public string Material { get; set; }
        public int MinBars { get; set; }
        public int MaxBars { get; set; }

        public decimal Price { get; set; }

        public bool isDelete { get; set; }
        public int? BirdCageTypeId { get; set; }

        public BirdCageType BirdCageType { get; set; }

        public int? ConstructionTime { get; set; }

        public virtual ICollection<FormulaSpecification> FormulaSpecifications { get; set; }


    }
}
