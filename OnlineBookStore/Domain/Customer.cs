namespace OnlineBookStore.Domain
{
    public class Customer : BaseDomainModel
    {
        // Link to ASP.NET Identity user
        public string? UserId { get; set; }

        public string? FullName { get; set; }
        public string? Email { get; set; }   // keep (useful for receipt/contact)
        public string? Phone { get; set; }
        public string? Address { get; set; }

        // Navigation
        public ICollection<Orders>? Orders { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
