using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;

namespace OnlineBookStore.Services;

/// <summary>
/// Admin-only payment management operations.
/// </summary>
public class AdminPaymentService
{
    private readonly IDbContextFactory<OnlineBookStoreContext> _dbFactory;

    public AdminPaymentService(IDbContextFactory<OnlineBookStoreContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Payment>> GetPaymentsAsync(string? search = null, string? status = null, int take = 300)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var q = db.Payment
            .AsNoTracking()
            .Include(p => p.Order!)
                .ThenInclude(o => o.Customer)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
            q = q.Where(p => p.PaymentStatus == status);

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            q = q.Where(p =>
                (p.PaymentMethod ?? "").Contains(search) ||
                (p.PaymentStatus ?? "").Contains(search) ||
                p.OrderId.ToString().Contains(search) ||
                (p.Order!.Customer!.FullName ?? "").Contains(search) ||
                (p.Order!.Customer!.Email ?? "").Contains(search));
        }

        return await q
            .OrderByDescending(p => p.DatePaid)
            .ThenByDescending(p => p.Id)
            .Take(take)
            .ToListAsync();
    }

    public async Task<Payment?> GetPaymentDetailsAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        return await db.Payment
            .Include(p => p.Order!)
                .ThenInclude(o => o.Customer)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdatePaymentAsync(Payment updated)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var existing = await db.Payment.FirstOrDefaultAsync(p => p.Id == updated.Id);
        if (existing == null) throw new Exception("Payment not found.");

        existing.PaymentMethod = updated.PaymentMethod;
        existing.PaymentStatus = updated.PaymentStatus;
        existing.Amount = updated.Amount;
        existing.DatePaid = updated.DatePaid;

        existing.DateUpdated = DateTime.Now;
        await db.SaveChangesAsync();
    }
}
