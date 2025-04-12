using static UITraining.Models.GeneralOrderStatus;

namespace UITraining.Models.DB
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key ke User
        public User User { get; set; }
        public string OrderCode { get; set; }
        public string Image { get; set; }
        public DateTime OrderDate { get; set; }
        public GeneralOrderStatusData Status { get; set; } // Contoh: "Pending", "Shipped", "Delivered"

        // Navigation properties
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
