using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;

namespace OnlineBookStore.Services
{
    public class OrderService
    {
        private readonly IDbContextFactory<OnlineBookStoreContext> _dbFactory;

        public OrderService(IDbContextFactory<OnlineBookStoreContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<int> PlaceOrderAsync(
            int customerId,
            IReadOnlyList<CartItem> cartItems,
            string paymentMethod)
        {
            if (cartItems == null || cartItems.Count == 0)
                throw new Exception("Your cart is empty.");

            if (string.IsNullOrWhiteSpace(paymentMethod))
                throw new Exception("Please select a payment method.");

            await using var db = await _dbFactory.CreateDbContextAsync();
            await using var tx = await db.Database.BeginTransactionAsync();

            // 1) Stock check (backend-trustworthy)
            foreach (var item in cartItems)
            {
                var book = await db.Book.FirstOrDefaultAsync(b => b.Id == item.BookId);
                if (book == null) throw new Exception("A book in your cart no longer exists.");

                if (book.StockQty < item.Quantity)
                    throw new Exception($"Not enough stock for: {book.Title}. Available: {book.StockQty}");
            }

            // 2) Create Order
            var order = new Orders
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                Status = OrderStatuses.Paid, // simulation (you can change to PendingPayment if you want)
                TotalAmount = cartItems.Sum(i => i.LineTotal)
            };

            db.Orders.Add(order);
            await db.SaveChangesAsync();

            // 3) Create OrderItems + reduce stock
            foreach (var item in cartItems)
            {
                var book = await db.Book.FirstAsync(b => b.Id == item.BookId);
                book.StockQty -= item.Quantity;

                db.OrderItem.Add(new OrderItem
                {
                    OrderId = order.Id,
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    LineTotal = item.LineTotal
                });
            }

            // 4) Create Payment
            db.Payment.Add(new Payment
            {
                OrderId = order.Id,
                PaymentMethod = paymentMethod,
                PaymentStatus = PaymentStatuses.Paid,
                Amount = order.TotalAmount,
                DatePaid = DateTime.Now
            });

            await db.SaveChangesAsync();
            await tx.CommitAsync();

            return order.Id;
        }

        public async Task<List<Orders>> GetOrdersForCustomerAsync(int customerId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();

            return await db.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.Payment)
                .Include(o => o.OrderItems!)
                    .ThenInclude(oi => oi.Book)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
