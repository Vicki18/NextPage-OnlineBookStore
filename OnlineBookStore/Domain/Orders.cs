namespace OnlineBookStore.Domain
{
    public class Orders : BaseDomainModel
    {
        public DateTime OrderDate { get; set; }
        public string? Status { get; set; }
        public decimal TotalAmount { get; set; }

        // FK
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        // Navigation
        public ICollection<OrderItem>? OrderItems { get; set; }
        public Payment? Payment { get; set; }
    }
}
