namespace UITraining.Models.DTO
{
    public class ProductSizeDTO
    {
        public int Id { get; set; }

        public string Size { get; set; } // Ukuran baju (S, M, L, XL, dll.)
        public int Stock { get; set; } // Stok untuk ukuran tertentu
        public string ProductName { get; set; }
        public int ProductId { get; set; } // Foreign key ke Product
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
