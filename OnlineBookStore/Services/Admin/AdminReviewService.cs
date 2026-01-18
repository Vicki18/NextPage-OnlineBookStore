using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;

namespace OnlineBookStore.Services
{
    public class AdminReviewService
    {
        private readonly IDbContextFactory<OnlineBookStoreContext> _dbFactory;

        public AdminReviewService(IDbContextFactory<OnlineBookStoreContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Review>> GetReviewsAsync(string? search)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            var q = db.Review
                .Include(r => r.Book)
                .Include(r => r.Customer)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();

                // allow rating search: "5" should match Rating == 5
                var isRating = int.TryParse(s, out var ratingValue);

                q = q.Where(r =>
                    (r.Book != null && (r.Book.Title ?? "").ToLower().Contains(s)) ||
                    (r.Customer != null && (r.Customer.FullName ?? "").ToLower().Contains(s)) ||
                    (r.Customer != null && (r.Customer.Email ?? "").ToLower().Contains(s)) ||
                    (r.Comment ?? "").ToLower().Contains(s) ||
                    (isRating && r.Rating == ratingValue)
                );
            }

            return await q
                .OrderByDescending(r => r.DateCreated)
                .ThenByDescending(r => r.Id)
                .ToListAsync();
        }

        public async Task<Review?> GetReviewAsync(int id)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Review
                .Include(r => r.Book)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateAsync(Review updated, string? updatedBy)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var r = await db.Review.FirstOrDefaultAsync(x => x.Id == updated.Id);

            if (r == null) throw new Exception("Review not found.");

            r.Rating = updated.Rating;
            r.Comment = updated.Comment;
            r.DateUpdated = DateTime.Now;
            r.UpdatedBy = updatedBy;

            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var r = await db.Review.FirstOrDefaultAsync(x => x.Id == id);

            if (r == null) throw new Exception("Review not found.");

            db.Review.Remove(r);
            await db.SaveChangesAsync();
        }
    }
}
