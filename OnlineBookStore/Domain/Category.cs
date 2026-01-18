namespace OnlineBookStore.Domain
{
    public class Category : BaseDomainModel
    {
        public string? CategoryName { get; set; }
        public string? Description { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
