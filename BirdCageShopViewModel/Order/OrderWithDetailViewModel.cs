using BirdCageShopViewModel.User;
using BirdCageShopViewModel.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Order
{
    public class OrderWithDetailViewModel
    {

        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? PaymentStatus { get; set; }


        //


        public VourcherViewModel Voucher { get; set; }
        public UserViewModel User { get; set; }
        public IEnumerable<OrderDetailViewModel> Details { get; set; }
    }
}
