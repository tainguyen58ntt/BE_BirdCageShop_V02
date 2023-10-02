using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            ProductReviews = new HashSet<ProductReview>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string? Address { get; set; }
        public string LastName { get; set; } = null!;
        public DateTime DoB { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Gender { get; set; }
        public int RoleId { get; set; }
        public bool IsDelete { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Wishlist? Wishlist { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
    }
}
