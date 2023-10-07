using System;
using System.Collections.Generic;
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
        public virtual DbSet<ProductFeature> ProductFeatures { get; set; } = null!;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<ProductReview> ProductReviews { get; set; } = null!;
        public virtual DbSet<ProductSpecification> ProductSpecifications { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Voucher> Vouchers { get; set; } = null!;
        public virtual DbSet<Wishlist> Wishlists { get; set; } = null!;
        public virtual DbSet<WishlistItem> WishlistItems { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ProductFeature>()
                .Property(pf => pf.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ProductSpecification>()
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
		}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<BankAccount>(entity =>
        //    {
        //        entity.Property(e => e.Id).ValueGeneratedNever();
        //    });

        //    modelBuilder.Entity<Category>(entity =>
        //    {
        //        entity.Property(e => e.CreateAt).HasColumnType("datetime");

        //        entity.Property(e => e.IsDelete).HasColumnName("isDelete");

        //        entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
        //    });

        //    modelBuilder.Entity<Order>(entity =>
        //    {
        //        entity.Property(e => e.Id).ValueGeneratedNever();

        //        entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

        //        entity.Property(e => e.OrderDate).HasColumnType("datetime");

        //        entity.Property(e => e.PaymentDate).HasColumnType("datetime");

        //        entity.Property(e => e.ShippingDate).HasColumnType("datetime");

        //        entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

        //        entity.HasOne(d => d.User)
        //            .WithMany(p => p.Orders)
        //            .HasForeignKey(d => d.UserId)
        //            .HasConstraintName("FK_Orders_Users");

        //        entity.HasOne(d => d.Voucher)
        //            .WithMany(p => p.Orders)
        //            .HasForeignKey(d => d.VoucherId)
        //            .HasConstraintName("FK_Orders_Vouchers");
        //    });

        //    modelBuilder.Entity<OrderDetail>(entity =>
        //    {
        //        entity.ToTable("OrderDetail");

        //        entity.Property(e => e.Id).ValueGeneratedNever();

        //        entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        //    });

        //    modelBuilder.Entity<Product>(entity =>
        //    {
        //        entity.Property(e => e.Id).ValueGeneratedNever();
        //    });

        //    modelBuilder.Entity<ProductFeature>(entity =>
        //    {
        //        entity.Property(e => e.CreatedAt)
        //            .HasColumnType("datetime")
        //            .HasColumnName("created_at");

        //        entity.Property(e => e.FeatureName).HasColumnName("feature_name");

        //        entity.Property(e => e.FeatureValue).HasColumnName("feature_value");

        //        entity.Property(e => e.ModiedAt)
        //            .HasColumnType("datetime")
        //            .HasColumnName("modied_at");

        //        entity.Property(e => e.Price)
        //            .HasColumnType("decimal(18, 2)")
        //            .HasColumnName("price");

        //        entity.Property(e => e.ProductId).HasColumnName("productId");
        //    });

        //    modelBuilder.Entity<ProductImage>(entity =>
        //    {
        //        entity.Property(e => e.Id).ValueGeneratedNever();

        //        entity.Property(e => e.CreatedAt).HasColumnType("datetime");

        //        entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");

        //        entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

        //        entity.HasOne(d => d.Product)
        //            .WithMany(p => p.ProductImages)
        //            .HasForeignKey(d => d.ProductId)
        //            .HasConstraintName("FK_ProductImages_Products");
        //    });

        //    modelBuilder.Entity<ProductReview>(entity =>
        //    {
        //        entity.Property(e => e.Id).ValueGeneratedNever();

        //        entity.Property(e => e.CreateAt).HasColumnName("Create_At");

        //        entity.Property(e => e.DeletedAt).HasColumnName("Deleted_At");

        //        entity.Property(e => e.IsDelete).HasColumnName("isDelete");

        //        entity.Property(e => e.ReviewDate).HasColumnType("datetime");

        //        entity.Property(e => e.UserId).HasColumnName("UserID");

        //        entity.HasOne(d => d.Product)
        //            .WithMany(p => p.ProductReviews)
        //            .HasForeignKey(d => d.ProductId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_ProductReviews_Products");

        //        entity.HasOne(d => d.User)
        //            .WithMany(p => p.ProductReviews)
        //            .HasForeignKey(d => d.UserId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_ProductReviews_Users");
        //    });

        //    modelBuilder.Entity<ProductSpecification>(entity =>
        //    {
        //        entity.Property(e => e.CreatedAt)
        //            .HasColumnType("datetime")
        //            .HasColumnName("created_at");

        //        entity.Property(e => e.ModiedAt)
        //            .HasColumnType("datetime")
        //            .HasColumnName("modied_at");

        //        entity.Property(e => e.Price)
        //            .HasColumnType("decimal(18, 2)")
        //            .HasColumnName("price");

        //        entity.Property(e => e.ProductId).HasColumnName("productId");

        //        entity.Property(e => e.SpecificationName).HasColumnName("specification_name");

        //        entity.Property(e => e.SpecificationValue).HasColumnName("specification_value");
        //    });

        //    modelBuilder.Entity<Role>(entity =>
        //    {
        //        entity.Property(e => e.CreateAt).HasColumnType("datetime");

        //        entity.Property(e => e.DeleteAt).HasColumnType("datetime");

        //        entity.Property(e => e.IsDelete).HasColumnName("isDelete");
        //    });

        //    modelBuilder.Entity<User>(entity =>
        //    {
        //        entity.Property(e => e.AvatarUrl).HasColumnName("AvatarURL");

        //        entity.Property(e => e.CreatedAt)
        //            .HasColumnType("datetime")
        //            .HasColumnName("Created_at");

        //        entity.Property(e => e.DeletedAt)
        //            .HasColumnType("datetime")
        //            .HasColumnName("Deleted_at");

        //        entity.Property(e => e.DoB).HasColumnType("datetime");

        //        entity.Property(e => e.IsDelete).HasColumnName("isDelete");

        //        entity.Property(e => e.ModifiedAt)
        //            .HasColumnType("datetime")
        //            .HasColumnName("Modified_at");

        //        entity.HasOne(d => d.Role)
        //            .WithMany(p => p.Users)
        //            .HasForeignKey(d => d.RoleId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Users_Roles");
        //    });

        //    modelBuilder.Entity<Voucher>(entity =>
        //    {
        //        entity.Property(e => e.Id).ValueGeneratedNever();

        //        entity.Property(e => e.DiscountPercent).HasColumnType("decimal(5, 2)");

        //        entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

        //        entity.Property(e => e.StartDate).HasColumnType("datetime");
        //    });

        //    modelBuilder.Entity<Wishlist>(entity =>
        //    {
        //        entity.HasIndex(e => e.UserId, "IX_Wishlists")
        //            .IsUnique();

        //        entity.Property(e => e.Id).ValueGeneratedNever();

        //        entity.Property(e => e.CreatedAt)
        //            .HasColumnType("datetime")
        //            .HasColumnName("Created_at");

        //        entity.Property(e => e.ModifiedAt)
        //            .HasColumnType("datetime")
        //            .HasColumnName("Modified_at");

        //        entity.HasOne(d => d.User)
        //            .WithOne(p => p.Wishlist)
        //            .HasForeignKey<Wishlist>(d => d.UserId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Wishlists_Users");
        //    });

        //    modelBuilder.Entity<WishlistItem>(entity =>
        //    {
        //        entity.Property(e => e.Id).ValueGeneratedNever();

        //        entity.HasOne(d => d.Product)
        //            .WithMany(p => p.WishlistItems)
        //            .HasForeignKey(d => d.ProductId)
        //            .HasConstraintName("FK_WishlistItems_Products");

        //        entity.HasOne(d => d.WishList)
        //            .WithMany(p => p.WishlistItems)
        //            .HasForeignKey(d => d.WishListId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_WishlistItems_Wishlists");
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
