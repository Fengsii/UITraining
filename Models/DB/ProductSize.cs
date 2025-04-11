namespace UITraining.Models.DB
{
    public class ProductSize
    {
        public int Id { get; set; }

        public string Size { get; set; } // Ukuran baju (S, M, L, XL, dll.)
        public int Stock { get; set; } // Stok untuk ukuran tertentu

        public int ProductId { get; set; } // Foreign key ke Product
        public Product2 Product { get; set; }
    }
}
