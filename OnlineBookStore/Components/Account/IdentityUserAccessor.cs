using OnlineBookStore.Data;
using Microsoft.AspNetCore.Identity;

namespace OnlineBookStore.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<OnlineBookStoreUser> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<OnlineBookStoreUser> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}
