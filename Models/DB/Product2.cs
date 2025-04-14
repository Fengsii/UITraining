using System.ComponentModel.DataAnnotations.Schema;
using static UITraining.Models.GeneralStatus;

namespace UITraining.Models.DB
{
    public class Product2
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; } // Foreign key ke Category
        public Category Category { get; set; }
        public GeneralStatusData ProductStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<ProductSize> Sizes { get; set; } // Relasi ke ProductSize
        public ICollection<OrderDetail> OrderDetails { get; set; } // Relasi ke OrderDetail
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Review> Reviews { get; set; } // Relasi ke Review

    }
}
