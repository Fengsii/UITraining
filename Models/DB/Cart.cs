namespace UITraining.Models.DB
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key ke User
        public User User { get; set; }
        public int ProductId { get; set; } // Foreign key ke Product
        public Product2 Product { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; } // Jumlah barang di keranjang
        public string SelectedSize { get; set; } // Ukuran yang dipilih
        public DateTime CreatedAt { get; set; }

    }
}
