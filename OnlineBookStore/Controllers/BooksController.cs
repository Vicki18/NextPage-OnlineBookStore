using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Controllers.Dto;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;
using OnlineBookStore.Services;

namespace OnlineBookStore.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IDbContextFactory<OnlineBookStoreContext> _dbFactory;
    private readonly AdminBookService _adminBookService;
    private readonly ReviewService _reviewService;

    public BooksController(
        IDbContextFactory<OnlineBookStoreContext> dbFactory,
        AdminBookService adminBookService,
        ReviewService reviewService)
    {
        _dbFactory = dbFactory;
        _adminBookService = adminBookService;
        _reviewService = reviewService;
    }

    // -----------------------------
    // Public (Store) endpoints
    // -----------------------------

    /// <summary>
    /// List books for the store catalog.
    /// Supports search, sort and pagination.
    /// Example: GET /api/books?search=harry&sort=price_desc&page=1&pageSize=12
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<object>> GetBooks(
        [FromQuery] string? search = null,
        [FromQuery] string sort = "title_asc",
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 24)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        await using var db = await _dbFactory.CreateDbContextAsync();
        var q = db.Book
            .AsNoTracking()
            .Include(b => b.Author)
            .Include(b => b.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim();
            q = q.Where(b =>
                (b.Title ?? "").Contains(s) ||
                (b.ISBN ?? "").Contains(s) ||
                (b.Author != null && (b.Author.Name ?? "").Contains(s)) ||
                (b.Category != null && (b.Category.CategoryName ?? "").Contains(s))
            );
        }

        IOrderedQueryable<Book> ordered = sort switch
        {
            "title_desc" => q.OrderByDescending(b => b.Title),
            "price_asc" => q.OrderBy(b => b.Price),
            "price_desc" => q.OrderByDescending(b => b.Price),
            "stock_asc" => q.OrderBy(b => b.StockQty),
            "stock_desc" => q.OrderByDescending(b => b.StockQty),
            _ => q.OrderBy(b => b.Title)
        };

        var total = await ordered.CountAsync();
        var items = await ordered
            .ThenByDescending(b => b.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(b => new BookDto(
                b.Id,
                b.Title,
                b.ISBN,
                b.Description,
                b.Price,
                b.StockQty,
                b.CoverImageUrl,
                b.AuthorId,
                b.Author != null ? b.Author.Name : null,
                b.CategoryId,
                b.Category != null ? b.Category.CategoryName : null
            ))
            .ToListAsync();

        return Ok(new
        {
            page,
            pageSize,
            total,
            items
        });
    }

    /// <summary>
    /// Book details.
    /// </summary>
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<BookDto>> GetBook(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var b = await db.Book
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (b is null) return NotFound();

        return Ok(new BookDto(
            b.Id,
            b.Title,
            b.ISBN,
            b.Description,
            b.Price,
            b.StockQty,
            b.CoverImageUrl,
            b.AuthorId,
            b.Author?.Name,
            b.CategoryId,
            b.Category?.CategoryName
        ));
    }

    /// <summary>
    /// Custom endpoint: get reviews for a book.
    /// Example: GET /api/books/5/reviews?take=20
    /// </summary>
    [HttpGet("{id:int}/reviews")]
    [AllowAnonymous]
    public async Task<ActionResult<List<ReviewDto>>> GetBookReviews(int id, [FromQuery] int take = 50)
    {
        var reviews = await _reviewService.GetReviewsForBookAsync(id, take);
        return Ok(reviews.Select(r => new ReviewDto(
            r.Id,
            r.BookId,
            r.CustomerId,
            r.Customer?.FullName,
            r.Customer?.Email,
            r.Rating,
            r.Comment,
            r.DateCreated
        )).ToList());
    }

    /// <summary>
    /// Custom endpoint: add a review for a book (customer-facing).
    /// Example: POST /api/books/5/reviews
    /// Body: { "rating": 5, "comment": "Nice" }
    /// </summary>
    [HttpPost("{id:int}/reviews")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult<ReviewDto>> AddReview(int id, [FromBody] ReviewCreateDto input)
    {
        try
        {
            var r = await _reviewService.AddReviewForCurrentUserAsync(User, id, input.Rating, input.Comment);
            var dto = new ReviewDto(r.Id, r.BookId, r.CustomerId, r.Customer?.FullName, r.Customer?.Email, r.Rating, r.Comment, r.DateCreated);
            return CreatedAtAction(nameof(GetBookReviews), new { id }, dto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    // -----------------------------
    // Admin endpoints (CRUD)
    // -----------------------------

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<object>> CreateBook([FromBody] BookUpsertDto input)
    {
        var book = new Book
        {
            Title = input.Title,
            ISBN = input.ISBN,
            Description = input.Description,
            Price = input.Price,
            StockQty = input.StockQty,
            CoverImageUrl = input.CoverImageUrl,
            AuthorId = input.AuthorId,
            CategoryId = input.CategoryId
        };

        var id = await _adminBookService.CreateAsync(book, User.Identity?.Name);
        return CreatedAtAction(nameof(GetBook), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpsertDto input)
    {
        try
        {
            var updated = new Book
            {
                Id = id,
                Title = input.Title,
                ISBN = input.ISBN,
                Description = input.Description,
                Price = input.Price,
                StockQty = input.StockQty,
                CoverImageUrl = input.CoverImageUrl,
                AuthorId = input.AuthorId,
                CategoryId = input.CategoryId
            };

            await _adminBookService.UpdateAsync(updated, User.Identity?.Name);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            await _adminBookService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            // e.g. book exists in order history => 409
            return Conflict(new { message = ex.Message });
        }
    }
}
