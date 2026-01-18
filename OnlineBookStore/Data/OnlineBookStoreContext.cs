using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Configurations.Entities;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;
using System.Reflection.Emit;

namespace OnlineBookStore.Data
{
    public class OnlineBookStoreContext(DbContextOptions<OnlineBookStoreContext> options) : IdentityDbContext<OnlineBookStoreUser>(options)
    {
        public DbSet<OnlineBookStore.Domain.Book> Book { get; set; } = default!;
        public DbSet<OnlineBookStore.Domain.Author> Author { get; set; } = default!;
        public DbSet<OnlineBookStore.Domain.Category> Category { get; set; } = default!;
        public DbSet<OnlineBookStore.Domain.Customer> Customer { get; set; } = default!;
        public DbSet<OnlineBookStore.Domain.OrderItem> OrderItem { get; set; } = default!;
        public DbSet<OnlineBookStore.Domain.Orders> Orders { get; set; } = default!;
        public DbSet<OnlineBookStore.Domain.Payment> Payment { get; set; } = default!;
        public DbSet<OnlineBookStore.Domain.Review> Review { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customer linked to Identity (1 user -> 1 customer profile)
            builder.Entity<Customer>()
                .HasIndex(c => c.UserId)
                .IsUnique()
                .HasFilter("[UserId] IS NOT NULL");

            // Catalog seeding (realistic: store starts with items)
            builder.ApplyConfiguration(new AuthorSeed());
            builder.ApplyConfiguration(new CategorySeed());
            builder.ApplyConfiguration(new BookSeed());

            // Decimal precision (good practice)
            builder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            builder.Entity<Book>()
                .Property(b => b.Price)
                .HasPrecision(18, 2);

            builder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2);

            builder.Entity<OrderItem>()
                .Property(oi => oi.LineTotal)
                .HasPrecision(18, 2);

            builder.Entity<Orders>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);
        }
    }
}
