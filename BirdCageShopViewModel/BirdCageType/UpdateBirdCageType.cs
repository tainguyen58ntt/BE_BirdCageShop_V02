using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.BirdCageType
{
    public class UpdateBirdCageType
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = null!;

        public string? Description { get; set; }
        /*public DateTime? CreateAt { get; set; }*/
        public DateTime? ModifiedAt { get; set; }
        /*public bool IsDelete { get; set; }*/

    }
}
