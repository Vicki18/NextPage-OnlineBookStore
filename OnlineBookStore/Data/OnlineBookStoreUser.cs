using Microsoft.AspNetCore.Identity;

namespace OnlineBookStore.Data
{
    
    public class OnlineBookStoreUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        
        public string? Address { get; set; }

        public string? DisplayName { get; set; }
    }
}
