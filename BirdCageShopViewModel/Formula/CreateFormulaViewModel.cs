using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Formula
{
    public class CreateFormulaViewModel
    {
        public string Code { get; set; }
        public int MinWidth { get; set; }
        public int MaxWidth { get; set; }
        public int MinHeight { get; set; }
        public int MaxHeight { get; set; }
//
        public int MinBars { get; set; }
        public int MaxBars { get; set; }

        public decimal Price { get; set; }
        public int BirdCageTypeId { get; set; }
        public int? ConstructionTime { get; set; }
        public ICollection<int> Specifications { get; set; }
    }
}
