using System;
using System.Collections.Generic;
using BirdCageShopDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BirdCageShopDbContext.Models
{
    public partial class BirdCageShopContext : DbContext
    {
       

        public BirdCageShopContext(DbContextOptions<BirdCageShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BankAccount> BankAccounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Feature> Feature { get; set; } = null!;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<ProductReview> ProductReviews { get; set; } = null!;
        public virtual DbSet<Specification> Specification { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Voucher> Vouchers { get; set; } = null!;
        public virtual DbSet<Wishlist> Wishlists { get; set; } = null!;
        public virtual DbSet<WishlistItem> WishlistItems { get; set; } = null!;
		public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        public virtual DbSet<BirdCageType> BirdCageTypes { get; set; } = null!;
        public virtual DbSet<ProductSpecification> ProductSpecifications { get; set; } = null!;
        public virtual DbSet<ProductFeature> ProductFeature { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Feature>()
                .Property(pf => pf.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Specification>()
                .Property(ps => ps.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Voucher>()
                .Property(v => v.DiscountPercent)
                .HasColumnType("decimal(18, 2)");

            // Configure the Voucher entity
            modelBuilder.Entity<Voucher>()
                .HasIndex(v => v.VoucherCode)
                .IsUnique();

			modelBuilder.Entity<Product>()
		   .HasOne(p => p.Category)
		   .WithMany(c => c.Products)
		   .HasForeignKey(p => p.CategoryId);

			modelBuilder.Entity<Product>()
	   .HasMany(p => p.ShoppingCarts)
	   .WithOne(sc => sc.Product)
	   .HasForeignKey(sc => sc.ProductId);

			modelBuilder.Entity<ShoppingCart>()
				.HasOne(sc => sc.Product)
				.WithMany(p => p.ShoppingCarts)
				.HasForeignKey(sc => sc.ProductId);

			modelBuilder.Entity<User>()
	.HasMany(u => u.ShoppingCarts)
	.WithOne(sc => sc.User)
	.HasForeignKey(sc => sc.UserId);
		}
     
    }
}
