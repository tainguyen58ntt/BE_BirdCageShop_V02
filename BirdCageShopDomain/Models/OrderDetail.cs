using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }

        public string? Model { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        //public string? Color { get; set; }
        public string? Material { get; set; }
        public int? Bars { get; set; }

    }
}
