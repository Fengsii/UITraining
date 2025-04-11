namespace UITraining.Models.DB
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Navigation properties
        public ICollection<Product2> Products { get; set; } // Relasi ke Product
    }
}
