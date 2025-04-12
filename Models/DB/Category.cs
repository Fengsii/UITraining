using static UITraining.Models.GeneralStatus;

namespace UITraining.Models.DB
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public GeneralStatusData CategoryStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // Navigation properties
        public ICollection<Product2> Products { get; set; } // Relasi ke Product
    }
}
