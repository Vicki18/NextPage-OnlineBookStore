using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;

namespace OnlineBookStore.Services;

/// <summary>
/// Admin-only order item management operations.
/// </summary>
public class AdminOrderItemService
{
    private readonly IDbContextFactory<OnlineBookStoreContext> _dbFactory;

    public AdminOrderItemService(IDbContextFactory<OnlineBookStoreContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<OrderItem>> GetOrderItemsAsync(string? search = null, int? orderId = null, int take = 400)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var q = db.OrderItem
            .AsNoTracking()
            .Include(oi => oi.Book)
            .Include(oi => oi.Order!)
                .ThenInclude(o => o.Customer)
            .AsQueryable();

        if (orderId.HasValue)
            q = q.Where(oi => oi.OrderId == orderId.Value);

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            q = q.Where(oi =>
                (oi.Book!.Title ?? "").Contains(search) ||
                (oi.Order!.Customer!.FullName ?? "").Contains(search) ||
                oi.OrderId.ToString().Contains(search) ||
                oi.BookId.ToString().Contains(search));
        }

        return await q
            .OrderByDescending(oi => oi.Id)
            .Take(take)
            .ToListAsync();
    }

    public async Task<OrderItem?> GetOrderItemAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.OrderItem
            .AsNoTracking()
            .Include(oi => oi.Book)
            .Include(oi => oi.Order!)
                .ThenInclude(o => o.Customer)
            .FirstOrDefaultAsync(oi => oi.Id == id);
    }
}
