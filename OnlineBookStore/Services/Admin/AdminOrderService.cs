using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;

namespace OnlineBookStore.Services;

public class AdminOrderService
{
    private readonly IDbContextFactory<OnlineBookStoreContext> _dbFactory;

    public AdminOrderService(IDbContextFactory<OnlineBookStoreContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Orders>> GetOrdersAsync(
        string? search = null,
        string? status = null,
        int take = 200)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var q = db.Orders
            .AsNoTracking()
            .Include(o => o.Customer)
            .Include(o => o.Payment)
            .AsQueryable();

        // Keep this for future (trim-safe)
        if (!string.IsNullOrWhiteSpace(status))
        {
            var wanted = status.Trim();
            q = q.Where(o => (o.Status ?? "").Trim() == wanted);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();

            q = q.Where(o =>
                EF.Functions.Like(o.Id.ToString(), $"%{search}%") ||
                (o.Customer != null && (
                    EF.Functions.Like(o.Customer.FullName ?? "", $"%{search}%") ||
                    EF.Functions.Like(o.Customer.Email ?? "", $"%{search}%")
                ))
            );
        }

        return await q
            .OrderByDescending(o => o.OrderDate)
            .Take(Math.Clamp(take, 1, 500))
            .ToListAsync();
    }

    public async Task<Orders?> GetOrderDetailsAsync(int orderId)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        return await db.Orders
            .AsNoTracking()
            .Include(o => o.Customer)
            .Include(o => o.Payment)
            .Include(o => o.OrderItems!)
                .ThenInclude(oi => oi.Book)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<List<string>> GetDistinctStatusesAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        return await db.Orders
            .AsNoTracking()
            .Where(o => o.Status != null && o.Status != "")
            .Select(o => o.Status!)
            .Distinct()
            .OrderBy(s => s)
            .ToListAsync();
    }

    public async Task UpdateOrderStatusAsync(int orderId, string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new Exception("Status cannot be empty.");

        await using var db = await _dbFactory.CreateDbContextAsync();

        var order = await db.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order is null) throw new Exception("Order not found.");

        order.Status = status.Trim();
        order.DateUpdated = DateTime.Now;

        await db.SaveChangesAsync();
    }

    public async Task UpsertPaymentAsync(
        int orderId,
        string? paymentMethod,
        string? paymentStatus,
        DateTime? datePaid)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var order = await db.Orders
            .Include(o => o.Payment)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order is null) throw new Exception("Order not found.");

        if (order.Payment is null)
        {
            order.Payment = new Payment
            {
                OrderId = orderId,
                Amount = order.TotalAmount,
                PaymentMethod = paymentMethod,
                PaymentStatus = paymentStatus,
                DatePaid = datePaid
            };
            db.Payment.Add(order.Payment);
        }
        else
        {
            order.Payment.PaymentMethod = paymentMethod;
            order.Payment.PaymentStatus = paymentStatus;
            order.Payment.DatePaid = datePaid;
            order.Payment.Amount = order.TotalAmount;
        }

        await db.SaveChangesAsync();
    }
}
