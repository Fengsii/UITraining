using static UITraining.Models.GeneralOrderStatus;
using UITraining.Models.DB;

namespace UITraining.Models.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public int UserId { get; set; } // Foreign key ke User
        public string UserName { get; set; }
        public string Image { get; set; }
        public DateTime OrderDate { get; set; }
        public GeneralOrderStatusData Status { get; set; }
    }
}
