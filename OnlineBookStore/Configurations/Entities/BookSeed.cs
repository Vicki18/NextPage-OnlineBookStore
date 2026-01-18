using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBookStore.Domain;

namespace OnlineBookStore.Configurations.Entities
{
    public class BookSeed : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book
                {
                    Id = 1,
                    Title = "The Silent Shelf",
                    ISBN = "978-1111111111",
                    Description = "A cozy fiction story set in a hidden bookstore.",
                    Price = 18.90m,
                    StockQty = 10,
                    CoverImageUrl = null,
                    AuthorId = 1,
                    CategoryId = 1,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Book
                {
                    Id = 2,
                    Title = "Data in the City",
                    ISBN = "978-2222222222",
                    Description = "Understanding patterns in real-world datasets.",
                    Price = 22.50m,
                    StockQty = 5,
                    CoverImageUrl = null,
                    AuthorId = 2,
                    CategoryId = 2,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Book
                {
                    Id = 3,
                    Title = "C# for Busy Students",
                    ISBN = "978-3333333333",
                    Description = "A practical guide to Blazor development.",
                    Price = 28.00m,
                    StockQty = 7,
                    CoverImageUrl = null,
                    AuthorId = 3,
                    CategoryId = 3,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                }
            );
        }
    }
}
