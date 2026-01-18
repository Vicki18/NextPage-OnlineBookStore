using OnlineBookStore.Domain;

namespace OnlineBookStore.Services
{
    public class CartItem
    {
        public int BookId { get; set; }
        public string Title { get; set; } = "";
        public string? CoverImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public decimal LineTotal => UnitPrice * Quantity;
    }

    public class CartService
    {
        private readonly List<CartItem> _items = new();

        public IReadOnlyList<CartItem> Items => _items;

        public void Add(Book book, int qty = 1)
        {
            if (qty < 1) qty = 1;

            var existing = _items.FirstOrDefault(i => i.BookId == book.Id);
            if (existing != null)
            {
                existing.Quantity += qty;
                return;
            }

            _items.Add(new CartItem
            {
                BookId = book.Id,
                Title = book.Title ?? "(Untitled)",
                CoverImageUrl = book.CoverImageUrl,
                UnitPrice = book.Price,
                Quantity = qty
            });
        }

        public void UpdateQty(int bookId, int qty)
        {
            var item = _items.FirstOrDefault(i => i.BookId == bookId);
            if (item == null) return;

            if (qty <= 0)
            {
                _items.Remove(item);
                return;
            }

            item.Quantity = qty;
        }

        public void Remove(int bookId)
        {
            var item = _items.FirstOrDefault(i => i.BookId == bookId);
            if (item != null) _items.Remove(item);
        }

        public void Clear() => _items.Clear();

        public decimal Total() => _items.Sum(i => i.LineTotal);

        public bool IsEmpty() => !_items.Any();
    }
}
