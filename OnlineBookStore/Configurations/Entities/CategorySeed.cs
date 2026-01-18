using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBookStore.Domain;

namespace OnlineBookStore.Configurations.Entities
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category {
                    Id = 1, 
                    CategoryName = "Fiction",
                    Description = "Novels and stories (literary, romance, mystery, fantasy).",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Category { 
                    Id = 2, 
                    CategoryName = "Non-Fiction",
                    Description = "Real-world topics (biography, business, self-help, history).",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Category {
                    Id = 3, 
                    CategoryName = "Education",
                    Description = "Learning materials (programming, textbooks, exam guides).",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                }
            );
        }
    }
}