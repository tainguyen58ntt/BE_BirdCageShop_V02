﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Specification
{
    public class SpecificationViewModel
    {
        public string SpecificationName { get; set; } = null!;
        public string SpecificationValue { get; set; } = null!;
        public decimal? Price { get; set; }
    }
}