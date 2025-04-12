using UITraining.Models.DB;

namespace UITraining.Models.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key ke User
        public string UserName { get; set; }
        public int ProductId { get; set; } // Foreign key ke Product
        public string ProductName { get; set; }
        public string Image { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // Skala 1-5
        public DateTime CreatedAt { get; set; }
    }
}
