namespace OnlineBookStore.Domain
{
    public class OrderItem : BaseDomainModel
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }

        public int OrderId { get; set; }
        public Orders? Order { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}
