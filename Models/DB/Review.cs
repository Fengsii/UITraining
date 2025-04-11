namespace UITraining.Models.DB
{
    public class Review
    {
        public int Id { get; set; }

        public int UserId { get; set; } // Foreign key ke User
        public User User { get; set; }

        public int ProductId { get; set; } // Foreign key ke Product
        public Product2 Product { get; set; }

        public string Comment { get; set; }
        public int Rating { get; set; } // Skala 1-5

        public DateTime CreatedAt { get; set; }

    }
}
