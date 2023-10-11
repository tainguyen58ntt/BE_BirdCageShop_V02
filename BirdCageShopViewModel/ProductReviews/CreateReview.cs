using BirdCageShopViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.ProductReviews
{
	public class CreateReview
	{
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int? Rating { get; set; }
        public string? ReviewText { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string? DeletedAt { get; set; }
        public string? CreateAt { get; set; }
        public bool IsDelete { get; set; }


	}
}
	