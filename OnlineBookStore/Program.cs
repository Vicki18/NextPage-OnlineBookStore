using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using OnlineBookStore.Components;
using OnlineBookStore.Components.Account;
using OnlineBookStore.Data;
using OnlineBookStore.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<OnlineBookStoreContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("OnlineBookStoreContext")
        ?? throw new InvalidOperationException("Connection string 'OnlineBookStoreContext' not found.")));

builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ✅ REST API Controllers (for Postman / integration testing)
builder.Services.AddControllers().AddJsonOptions(o =>
{
    // Avoid circular reference issues when serializing EF navigation properties.
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

// ✅ These 4 types are in Components/Account (your project already has them)
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddSingleton<IEmailSender<OnlineBookStoreUser>, IdentityNoOpEmailSender>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies();

// ✅ For school project: allow login without email confirmation
builder.Services.AddIdentityCore<OnlineBookStoreUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<OnlineBookStoreContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// App services
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ReviewService>();

builder.Services.AddScoped<AdminOrderService>();
builder.Services.AddScoped<AdminOrderItemService>();
builder.Services.AddScoped<AdminPaymentService>();
builder.Services.AddScoped<AdminCustomerService>();
builder.Services.AddScoped<AdminBookService>();
builder.Services.AddScoped<AdminAuthorService>();
builder.Services.AddScoped<AdminCategoryService>();
builder.Services.AddScoped<AdminReviewService>();

var app = builder.Build();

// ✅ Ensure DB exists + seed roles + seed accounts
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var db = services.GetRequiredService<OnlineBookStoreContext>();
    await db.Database.MigrateAsync();

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<OnlineBookStoreUser>>();

    // Create roles if not exist
    if (!await roleManager.RoleExistsAsync("Administrator"))
        await roleManager.CreateAsync(new IdentityRole("Administrator"));

    if (!await roleManager.RoleExistsAsync("User"))
        await roleManager.CreateAsync(new IdentityRole("User"));

    // Seed Admin
    var adminEmail = "admin@localhost.com";
    var admin = await userManager.FindByEmailAsync(adminEmail);

    if (admin == null)
    {
        admin = new OnlineBookStoreUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "User"
        };

        var createAdmin = await userManager.CreateAsync(admin, "NextP@ge1");
        if (!createAdmin.Succeeded)
            throw new Exception("Failed to create admin user: " +
                                string.Join("; ", createAdmin.Errors.Select(e => e.Description)));
    }

    if (!await userManager.IsInRoleAsync(admin, "Administrator"))
        await userManager.AddToRoleAsync(admin, "Administrator");

    // Seed normal User (optional test account)
    var userEmail = "user@localhost.com";
    var user = await userManager.FindByEmailAsync(userEmail);

    if (user == null)
    {
        user = new OnlineBookStoreUser
        {
            UserName = userEmail,
            Email = userEmail,
            EmailConfirmed = true,
            FirstName = "Test",
            LastName = "Customer"
        };

        var createUser = await userManager.CreateAsync(user, "User123!");
        if (!createUser.Succeeded)
            throw new Exception("Failed to create normal user: " +
                                string.Join("; ", createUser.Errors.Select(e => e.Description)));
    }

    if (!await userManager.IsInRoleAsync(user, "User"))
        await userManager.AddToRoleAsync(user, "User");
}

// ✅ Correct environment pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// ✅ REQUIRED for [Authorize] on API controllers
app.UseAuthentication();
app.UseAuthorization();

// ✅ REST API endpoints
app.MapControllers();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();
