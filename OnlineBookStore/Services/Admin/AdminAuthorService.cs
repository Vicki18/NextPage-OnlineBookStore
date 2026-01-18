using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;

namespace OnlineBookStore.Services;

public class AdminAuthorService
{
    private readonly IDbContextFactory<OnlineBookStoreContext> _dbFactory;

    public AdminAuthorService(IDbContextFactory<OnlineBookStoreContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Author>> GetAuthorsAsync(string? search = null)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var q = db.Author
            .AsNoTracking()
            .Include(a => a.Books!)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            q = q.Where(a =>
                (a.Name ?? "").Contains(search) ||
                (a.Bio ?? "").Contains(search));
        }

        return await q
            .OrderBy(a => a.Name)
            .ThenByDescending(a => a.Id)
            .ToListAsync();
    }

    public async Task<Author?> GetAuthorAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Author
            .AsNoTracking()
            .Include(a => a.Books!)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<int> CreateAsync(Author author, string? createdBy)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        author.Name = (author.Name ?? "").Trim();
        author.Bio = (author.Bio ?? "").Trim();
        author.CreatedBy = createdBy;
        author.DateCreated = DateTime.Now;

        db.Author.Add(author);
        await db.SaveChangesAsync();
        return author.Id;
    }

    public async Task UpdateAsync(Author updated, string? updatedBy)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        var existing = await db.Author.FirstOrDefaultAsync(a => a.Id == updated.Id);
        if (existing is null) throw new Exception("Author not found.");

        existing.Name = (updated.Name ?? "").Trim();
        existing.Bio = (updated.Bio ?? "").Trim();
        existing.UpdatedBy = updatedBy;
        existing.DateUpdated = DateTime.Now;

        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var author = await db.Author
            .Include(a => a.Books!)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (author is null) return;

        // Safety: cannot delete if books still linked
        if (author.Books != null && author.Books.Count > 0)
            throw new Exception("Cannot delete this author because books are linked to them.");

        db.Author.Remove(author);
        await db.SaveChangesAsync();
    }
}
