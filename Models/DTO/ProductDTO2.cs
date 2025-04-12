using static UITraining.Models.GeneralStatus;
using System.ComponentModel.DataAnnotations.Schema;
using UITraining.Models.DB;

namespace UITraining.Models.DTO
{
    public class ProductDTO2
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; } // Total stok keseluruhan
        public bool? IsPromo { get; set; } // Apakah produk sedang promo?
        public decimal? Discount { get; set; } // Diskon dalam persen (misal: 10 untuk 10%)
        public string CategoryName { get; set; }
        public int CategoryId { get; set; } // Foreign key ke Category
        public GeneralStatusData ProductStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
