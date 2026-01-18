namespace OnlineBookStore.Services
{
    public static class OrderStatuses
    {
        public const string PendingPayment = "PendingPayment";
        public const string Paid = "Paid";
        public const string Processing = "Processing";
        public const string Shipped = "Shipped";
        public const string Completed = "Completed";
        public const string Cancelled = "Cancelled";
        public const string Refunded = "Refunded";
    }

    public static class PaymentStatuses
    {
        public const string Pending = "Pending";
        public const string Paid = "Paid";
        public const string Failed = "Failed";
        public const string Refunded = "Refunded";
    }

    public static class PaymentMethods
    {
        public const string Card = "Card";
        public const string PayNow = "PayNow";
        public const string CashOnDelivery = "CashOnDelivery";
    }
}
