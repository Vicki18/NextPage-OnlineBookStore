namespace OnlineBookStore.Domain
{
    public class Author : BaseDomainModel
    {
        public string? Name { get; set; }
        public string? Bio { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
