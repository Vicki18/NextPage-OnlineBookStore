using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;

namespace OnlineBookStore.Services
{
    public class CustomerService
    {
        private readonly IDbContextFactory<OnlineBookStoreContext> _dbFactory;
        private readonly UserManager<OnlineBookStoreUser> _userManager;

        public CustomerService(
            IDbContextFactory<OnlineBookStoreContext> dbFactory,
            UserManager<OnlineBookStoreUser> userManager)
        {
            _dbFactory = dbFactory;
            _userManager = userManager;
        }

        public async Task<Customer> GetOrCreateCustomerForCurrentUserAsync(
            System.Security.Claims.ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal)
                       ?? throw new InvalidOperationException("User not logged in.");

            // Optional safety: prevent admin becoming a customer profile
            if (await _userManager.IsInRoleAsync(user, "Administrator"))
                throw new InvalidOperationException("Administrator account does not have a customer profile.");

            var userId = user.Id;

            await using var db = await _dbFactory.CreateDbContextAsync();

            var customer = await db.Customer.FirstOrDefaultAsync(c => c.UserId == userId);
            if (customer != null)
            {
                // Keep email/name in sync (light sync)
                var desiredEmail = (user.Email ?? "").Trim();
                var desiredName = $"{user.FirstName} {user.LastName}".Trim();

                bool changed = false;
                if (!string.Equals((customer.Email ?? "").Trim(), desiredEmail, StringComparison.OrdinalIgnoreCase))
                {
                    customer.Email = desiredEmail;
                    changed = true;
                }
                if (!string.Equals((customer.FullName ?? "").Trim(), desiredName, StringComparison.Ordinal))
                {
                    customer.FullName = desiredName;
                    changed = true;
                }

                if (changed) await db.SaveChangesAsync();
                return customer;
            }

            // Create (1 user -> 1 customer)
            customer = new Customer
            {
                UserId = userId,
                Email = (user.Email ?? "").Trim(),
                FullName = $"{user.FirstName} {user.LastName}".Trim(),
                Phone = (user.PhoneNumber ?? "").Trim(),
                Address = (user.Address ?? "").Trim()
            };

            db.Customer.Add(customer);
            await db.SaveChangesAsync();
            return customer;
        }

        /// <summary>
        /// Update BOTH Customer table (admin pages) and Identity user fields (Account/Manage pages).
        /// This prevents "user updated but admin still sees old info".
        /// </summary>
        public async Task<Customer> UpdateCustomerProfileAsync(
            int customerId,
            string fullName,
            string phone,
            string address)
        {
            fullName = (fullName ?? "").Trim();
            phone = (phone ?? "").Trim();
            address = (address ?? "").Trim();

            await using var db = await _dbFactory.CreateDbContextAsync();
            var customer = await db.Customer.FirstOrDefaultAsync(c => c.Id == customerId)
                           ?? throw new Exception("Customer profile not found.");

            // Update Customer table (admin reads this)
            customer.FullName = string.IsNullOrWhiteSpace(fullName) ? customer.FullName : fullName;
            customer.Phone = string.IsNullOrWhiteSpace(phone) ? customer.Phone : phone;
            customer.Address = string.IsNullOrWhiteSpace(address) ? customer.Address : address;

            await db.SaveChangesAsync();

            // Sync to Identity user too (so Account/Manage is consistent)
            if (!string.IsNullOrWhiteSpace(customer.UserId))
            {
                var user = await _userManager.FindByIdAsync(customer.UserId);
                if (user != null)
                {
                    // If you store FirstName/LastName separately, best-effort split:
                    if (!string.IsNullOrWhiteSpace(fullName))
                    {
                        var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 1)
                        {
                            user.FirstName = parts[0];
                            user.LastName = "";
                        }
                        else
                        {
                            user.FirstName = parts[0];
                            user.LastName = string.Join(' ', parts.Skip(1));
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(phone))
                        user.PhoneNumber = phone;

                    if (!string.IsNullOrWhiteSpace(address))
                        user.Address = address;

                    // Email should normally be updated only from Identity UI,
                    // so we do NOT change user.Email here.
                    await _userManager.UpdateAsync(user);
                }
            }

            return customer;
        }
    }
}
