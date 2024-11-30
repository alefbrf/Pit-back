using CoffeeBreak.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBreak.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<ProductCart> ProductsCart { get; set; }
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderProduct> OrderProducts { get; set; } 
        public DbSet<Configs> Configs { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VerificationCode>(entity =>
            {
                entity.HasOne(e => e.User).WithMany(p => p.VerificationCodes)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Verification_Code_User0_Id");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasOne(e => e.User).WithMany(p => p.Addresses)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Address_User0_Id");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price).HasPrecision(7, 2);
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasOne(e => e.User).WithMany(p => p.Favorites)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Favorite_User0_Id");

                entity.HasOne(e => e.Product).WithMany(p => p.Favorites)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Favorite_Product0_Id");
            });

            modelBuilder.Entity<ProductCart>(entity =>
            {
                entity.HasOne(e => e.User).WithMany(p => p.ProductsCart)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Product_Cart_User0_Id");

                entity.HasOne(e => e.Product).WithMany(p => p.ProductsCart)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Product_Cart_Product0_Id");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(e => e.User).WithMany(e => e.Orders)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Order_User0_Id");

                entity.HasOne(e => e.DeliveryMan).WithMany(e => e.DeliveryOrders)
                    .HasForeignKey(e => e.DeliveryManId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User1_Id");

                entity.Property(e => e.Price).HasPrecision(7, 2);
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasOne(e => e.Order).WithMany(e => e.OrderProducts)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Order_Order0_Id");
            });

            modelBuilder.Entity<Configs>(entity =>
            {
                entity.Property(e => e.DeliveryTax).HasPrecision(7, 2);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
