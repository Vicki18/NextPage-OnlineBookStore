namespace OnlineBookStore.Domain
{
    public class Payment : BaseDomainModel
    {
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DatePaid { get; set; }

        public int OrderId { get; set; }
        public Orders? Order { get; set; }
    }
}
