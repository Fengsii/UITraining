using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Services;

var builder = WebApplication.CreateBuilder(args);




var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

// Replace 'YourDbContext' with the name of your own DbContext derived class.
builder.Services.AddDbContext<ApplicationContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(builder.Configuration.GetConnectionString("MySQLconnection"), serverVersion)
    // The following three options help with debugging, but should
    // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

builder.Services.AddScoped<IProduct,ProductService>();
builder.Services.AddScoped<ISupplier, SupplierService>();
builder.Services.AddScoped<IUserAccess, UserAccessService>();

//============== YANG BARU DITAMBAHKAN ==============\\
builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddHttpContextAccessor(); // Untuk mengakses HttpContext
builder.Services.AddScoped<IUser, UserService>(); // Mendaftarkan UserService
builder.Services.AddScoped<IProduct2, Product2Service>();
builder.Services.AddScoped<ICatagory, CategoryService>();
builder.Services.AddScoped<IReview, ReviewService>();





// Tambahkan layanan autentikasi dengan cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/UserAccess/Login"; // Redirect ke halaman login jika tidak terautentikasi
        options.AccessDeniedPath = "/Home/AccessDenied"; // Redirect ke halaman akses ditolak jika tidak memiliki izin
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Waktu kedaluwarsa cookie
        options.SlidingExpiration = true; // Perpanjang waktu kedaluwarsa cookie secara otomatis
    });

// Tambahkan layanan authorization
builder.Services.AddAuthorization();



// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Pastikan ini dipanggil sebelum UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
//pattern: "{controller=Dashboard}/{action=Index}/{id?}");
//pattern: "{controller=UserAccess}/{action=Login}/{id?}");
pattern: "{controller=Admin}/{action=LoginUser}/{id?}");

app.Run();
