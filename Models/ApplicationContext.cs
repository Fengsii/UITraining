using Microsoft.EntityFrameworkCore;
using UITraining.Helper;
using UITraining.Models.DB;

namespace UITraining.Models
{
    public class ApplicationContext : DbContext
    {

        private readonly IConfiguration _configuration;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<UserAccess> UserAccesses { get; set; }

        // User tables
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserBalance> UserBalances { get; set; }
        public virtual DbSet<Product2> Product2s { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ProductSize> ProductSizes { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)//
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.IdSupplier);// IdSuppiler
                                                  //.OnDelete(DeleteBehavior.Cascade);


            // User-Balance one-to-one relationship
            modelBuilder.Entity<UserBalance>()
                .HasOne(ub => ub.User)
                .WithOne(u => u.Balance)
                .HasForeignKey<UserBalance>(ub => ub.UserId);

            // Product-Category one-to-many relationship
            modelBuilder.Entity<Product2>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            // Product-ProductSize one-to-many relationship
            modelBuilder.Entity<ProductSize>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.Sizes)
                .HasForeignKey(ps => ps.ProductId);

            // Cart-User many-to-one relationship
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);

            // Cart-Product many-to-one relationship
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId);

            // Order-User many-to-one relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId);

            // OrderDetail-Order many-to-one relationship
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            // OrderDetail-Product many-to-one relationship
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId);


            // Review-User many-to-one relationship
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            // Review-Product many-to-one relationship
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);

            // Seeding admin default with SHA512 hashing
            var pepper = _configuration["Security:Papper"];
            var iteration = Convert.ToInt32(_configuration["Security:Iteration"]);
            var salt = Hasher.GenerateSalt();

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Administrator",
                    Username = "admin",
                    Email = "admin@example.com",
                    Salt = salt,
                    PasswordHash = Hasher.ComputeHash("admin123", salt, pepper, iteration),
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow,
                    UserStatus = GeneralStatus.GeneralStatusData.Published
                }
            );


            base.OnModelCreating(modelBuilder);
            
        }

    }
}
