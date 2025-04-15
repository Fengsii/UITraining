using Microsoft.EntityFrameworkCore;
using UITraining.Models.DB;

namespace UITraining.Models
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<ProductSize> ProductSizes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)//
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.IdSupplier);// IdSuppiler
                                                  //.OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductSize>()
               .HasOne(p => p.Product)//
               .WithMany(p => p.ProductsSizes)
               .HasForeignKey(p => p.ProductId);// IdSuppiler


            base.OnModelCreating(modelBuilder);
            
        }

    }
}
