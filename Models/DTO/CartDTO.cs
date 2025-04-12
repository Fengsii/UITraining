using UITraining.Models.DB;

namespace UITraining.Models.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key ke User
        public string UserName { get; set; }
        public int ProductId { get; set; } // Foreign key ke Product
        public string ProductName { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; } // Jumlah barang di keranjang
        public string SelectedSize { get; set; } // Ukuran yang dipilih
        public DateTime CreatedAt { get; set; }
    }
}
