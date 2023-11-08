using BirdCageShopDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopDomain.Models
{
    public class FormulaSpecification
    {
        public int Id { get; set; }
        public int FormulaId { get; set; }
        public int SpecificationId { get; set; }

        public Formula Formula { get; set; }
        public Specification Specification { get; set; }
    }
}
