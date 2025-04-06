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
