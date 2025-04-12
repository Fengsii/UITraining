namespace UITraining.Models.DB
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Foreign key ke Order
        public Order Order { get; set; }
        public int ProductId { get; set; } // Foreign key ke Product
        public Product2 Product { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public string SelectedSize { get; set; }
        public decimal PriceAtPurchase { get; set; } // Harga saat pembelian
    }
}
