using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public decimal? DiscountPercent { get; set; }
        public string VoucherCode { get; set; } = null!;
        public DateTime? ExpirationDate { get; set; }
        public DateTime StartDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
