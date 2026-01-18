using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;
using System.Linq;

namespace OnlineBookStore.Services;

public class AdminBookService
{
    private readonly IDbContextFactory<OnlineBookStoreContext> _dbFactory;

    public AdminBookService(IDbContextFactory<OnlineBookStoreContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Book>> GetBooksAsync(string? search = null, string sort = "title_asc")
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var q = db.Book
            .AsNoTracking()
            .Include(b => b.Author)
            .Include(b => b.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            q = q.Where(b =>
                (b.Title ?? "").Contains(search) ||
                (b.ISBN ?? "").Contains(search) ||
                (b.Author!.Name ?? "").Contains(search) ||
                (b.Category!.CategoryName ?? "").Contains(search));
        }

        // IMPORTANT: make sure ordering result is IOrderedQueryable<Book>
        IOrderedQueryable<Book> ordered = sort switch
        {
            "title_desc" => q.OrderByDescending(b => b.Title),
            "price_asc" => q.OrderBy(b => b.Price),
            "price_desc" => q.OrderByDescending(b => b.Price),
            "stock_asc" => q.OrderBy(b => b.StockQty),
            "stock_desc" => q.OrderByDescending(b => b.StockQty),
            _ => q.OrderBy(b => b.Title)
        };

        return await ordered
            .ThenByDescending(b => b.Id)
            .ToListAsync();
    }

    public async Task<Book?> GetBookAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        return await db.Book
            .AsNoTracking()
            .Include(b => b.Author)
            .Include(b => b.Category)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<List<Author>> GetAuthorsAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Author.AsNoTracking().OrderBy(a => a.Name).ToListAsync();
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Category.AsNoTracking().OrderBy(c => c.CategoryName).ToListAsync();
    }

    public async Task<int> CreateAsync(Book book, string? createdBy)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        Normalize(book);
        book.CreatedBy = createdBy;
        book.DateCreated = DateTime.Now;

        db.Book.Add(book);
        await db.SaveChangesAsync();
        return book.Id;
    }

    public async Task UpdateAsync(Book updated, string? updatedBy)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var existing = await db.Book.FirstOrDefaultAsync(b => b.Id == updated.Id);
        if (existing is null) throw new Exception("Book not found.");

        Normalize(updated);

        existing.Title = updated.Title;
        existing.ISBN = updated.ISBN;
        existing.Description = updated.Description;
        existing.Price = updated.Price;
        existing.StockQty = updated.StockQty;
        existing.CoverImageUrl = updated.CoverImageUrl;
        existing.AuthorId = updated.AuthorId;
        existing.CategoryId = updated.CategoryId;

        existing.UpdatedBy = updatedBy;
        existing.DateUpdated = DateTime.Now;

        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var book = await db.Book
            .Include(b => b.OrderItems!)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book is null) return;

        if (book.OrderItems != null && book.OrderItems.Count > 0)
            throw new Exception("Cannot delete this book because it exists in order history. Set StockQty = 0 instead.");

        db.Book.Remove(book);
        await db.SaveChangesAsync();
    }

    private static void Normalize(Book b)
    {
        b.Title = (b.Title ?? "").Trim();
        b.ISBN = (b.ISBN ?? "").Trim();
        b.Description = (b.Description ?? "").Trim();
        b.CoverImageUrl = (b.CoverImageUrl ?? "").Trim();

        if (b.Price < 0) b.Price = 0;
        if (b.StockQty < 0) b.StockQty = 0;
    }
}
