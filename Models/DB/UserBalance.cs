using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UITraining.Models.DB
{
    public class UserBalance
    {
        [Key]
        public int UserId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; } = 0;

        public DateTime LastUpdated { get; set; }

        public User User { get; set; }
    }
}
