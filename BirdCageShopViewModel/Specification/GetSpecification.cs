using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Specification
{
    public class GetSpecification
    {
        public int Id { get; set; }
        public string SpecificationName { get; set; } = null!;
        public string SpecificationValue { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModiedAt { get; set; }
    }
}
