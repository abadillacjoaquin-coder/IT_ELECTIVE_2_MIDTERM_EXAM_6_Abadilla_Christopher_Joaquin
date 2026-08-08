using Microsoft.AspNetCore.Authentication.Cookies;
using Package_Pickup_Monitoring_System.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add MVC services
builder.Services.AddControllersWithViews();

// Register Repositories (Singleton for in-memory persistence during app run)
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPackageRepository, PackageRepository>();

// Configure Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Package}/{action=Index}/{id?}");

app.Run();