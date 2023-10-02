using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductImages = new HashSet<ProductImage>();
            ProductReviews = new HashSet<ProductReview>();
            WishlistItems = new HashSet<WishlistItem>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public int CategoryId { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
