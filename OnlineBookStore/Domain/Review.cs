namespace OnlineBookStore.Domain
{
    public class Review : BaseDomainModel
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }

        // FK
        public int BookId { get; set; }
        public Book? Book { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
