using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class Wishlist
    {
        public Wishlist()
        {
            WishlistItems = new HashSet<WishlistItem>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
