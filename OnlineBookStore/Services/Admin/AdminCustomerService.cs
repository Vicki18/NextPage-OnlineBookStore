using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;

namespace OnlineBookStore.Services;

/// <summary>
/// Admin-only customer management operations.
/// </summary>
public class AdminCustomerService
{
    private readonly IDbContextFactory<OnlineBookStoreContext> _dbFactory;

    public AdminCustomerService(IDbContextFactory<OnlineBookStoreContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Customer>> GetCustomersAsync(string? search = null, int take = 300)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var q = db.Customer
            .AsNoTracking()
            .Include(c => c.Orders!)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            q = q.Where(c =>
                (c.FullName ?? "").Contains(search) ||
                (c.Email ?? "").Contains(search) ||
                (c.Phone ?? "").Contains(search) ||
                (c.Address ?? "").Contains(search) ||
                (c.UserId ?? "").Contains(search));
        }

        return await q
            .OrderByDescending(c => c.Id)
            .Take(take)
            .ToListAsync();
    }

    public async Task<Customer?> GetCustomerDetailsAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        return await db.Customer
            .AsNoTracking()
            .Include(c => c.Orders!)
                .ThenInclude(o => o.Payment)
            .Include(c => c.Orders!)
                .ThenInclude(o => o.OrderItems!)
                    .ThenInclude(oi => oi.Book)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task UpdateCustomerAsync(Customer updated)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var existing = await db.Customer.FirstOrDefaultAsync(c => c.Id == updated.Id);
        if (existing is null) throw new Exception("Customer not found.");

        existing.FullName = (updated.FullName ?? "").Trim();
        existing.Email = (updated.Email ?? "").Trim();
        existing.Phone = (updated.Phone ?? "").Trim();
        existing.Address = (updated.Address ?? "").Trim();

        existing.DateUpdated = DateTime.Now;
        await db.SaveChangesAsync();
    }
}
