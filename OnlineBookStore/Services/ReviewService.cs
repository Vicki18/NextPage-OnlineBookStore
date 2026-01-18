using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;

namespace OnlineBookStore.Services;

/// <summary>
/// Customer-facing review operations (used by REST API and can be reused by Razor pages).
/// Keeps "Application Layer" clean by centralizing business rules.
/// </summary>
public class ReviewService
{
    private readonly IDbContextFactory<OnlineBookStoreContext> _dbFactory;
    private readonly UserManager<OnlineBookStoreUser> _userManager;
    private readonly CustomerService _customerService;

    public ReviewService(
        IDbContextFactory<OnlineBookStoreContext> dbFactory,
        UserManager<OnlineBookStoreUser> userManager,
        CustomerService customerService)
    {
        _dbFactory = dbFactory;
        _userManager = userManager;
        _customerService = customerService;
    }

    public async Task<List<Review>> GetReviewsForBookAsync(int bookId, int take = 50)
    {
        take = Math.Clamp(take, 1, 200);

        await using var db = await _dbFactory.CreateDbContextAsync();

        return await db.Review
            .AsNoTracking()
            .Where(r => r.BookId == bookId)
            .Include(r => r.Customer)
            .OrderByDescending(r => r.DateCreated)
            .Take(take)
            .ToListAsync();
    }

    /// <summary>
    /// Adds a review for the current signed-in user.
    /// If the user doesn't have a Customer profile, it will be created.
    /// </summary>
    public async Task<Review> AddReviewForCurrentUserAsync(
        System.Security.Claims.ClaimsPrincipal principal,
        int bookId,
        int rating,
        string? comment)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");

        // Validate user + customer profile
        var identityUser = await _userManager.GetUserAsync(principal)
                           ?? throw new InvalidOperationException("User not logged in.");

        // Ensure a customer exists and is linked to this user
        var customer = await _customerService.GetOrCreateCustomerForCurrentUserAsync(principal);

        await using var db = await _dbFactory.CreateDbContextAsync();

        // Validate book existence
        var bookExists = await db.Book.AsNoTracking().AnyAsync(b => b.Id == bookId);
        if (!bookExists) throw new KeyNotFoundException("Book not found.");

        // Rule: one review per customer per book (simple + realistic)
        var already = await db.Review
            .AnyAsync(r => r.BookId == bookId && r.CustomerId == customer.Id);

        if (already)
            throw new InvalidOperationException("You have already reviewed this book.");

        var review = new Review
        {
            BookId = bookId,
            CustomerId = customer.Id,
            Rating = rating,
            Comment = (comment ?? "").Trim(),
            CreatedBy = identityUser.Email,
            DateCreated = DateTime.Now
        };

        db.Review.Add(review);
        await db.SaveChangesAsync();

        // Load navigations for response
        await db.Entry(review).Reference(r => r.Customer).LoadAsync();
        return review;
    }
}
