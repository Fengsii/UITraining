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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");
//pattern: "{controller=UserAccess}/{action=RegisterUser}/{id?}");

app.Run();
