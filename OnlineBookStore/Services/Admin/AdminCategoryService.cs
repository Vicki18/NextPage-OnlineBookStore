using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;

namespace OnlineBookStore.Services;

public class AdminCategoryService
{
    private readonly IDbContextFactory<OnlineBookStoreContext> _dbFactory;

    public AdminCategoryService(IDbContextFactory<OnlineBookStoreContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Category>> GetCategoriesAsync(string? search = null)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var q = db.Category
            .AsNoTracking()
            .Include(c => c.Books!)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            q = q.Where(c =>
                (c.CategoryName ?? "").Contains(search) ||
                (c.Description ?? "").Contains(search));
        }

        return await q
            .OrderBy(c => c.CategoryName)
            .ThenByDescending(c => c.Id)
            .ToListAsync();
    }

    public async Task<Category?> GetCategoryAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Category
            .AsNoTracking()
            .Include(c => c.Books!)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<int> CreateAsync(Category category, string? createdBy)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        category.CategoryName = (category.CategoryName ?? "").Trim();
        category.Description = (category.Description ?? "").Trim();
        category.CreatedBy = createdBy;
        category.DateCreated = DateTime.Now;

        db.Category.Add(category);
        await db.SaveChangesAsync();
        return category.Id;
    }

    public async Task UpdateAsync(Category updated, string? updatedBy)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        var existing = await db.Category.FirstOrDefaultAsync(c => c.Id == updated.Id);
        if (existing is null) throw new Exception("Category not found.");

        existing.CategoryName = (updated.CategoryName ?? "").Trim();
        existing.Description = (updated.Description ?? "").Trim();
        existing.UpdatedBy = updatedBy;
        existing.DateUpdated = DateTime.Now;

        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var category = await db.Category
            .Include(c => c.Books!)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null) return;

        if (category.Books != null && category.Books.Count > 0)
            throw new Exception("Cannot delete this category because books are linked to it.");

        db.Category.Remove(category);
        await db.SaveChangesAsync();
    }
}
