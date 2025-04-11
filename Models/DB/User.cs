using System.ComponentModel.DataAnnotations;
using static UITraining.Models.GeneralStatus;

namespace UITraining.Models.DB
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; } // Di-hash
        public string Role { get; set; } // "Admin" atau "User"
        public GeneralStatusData UserStatus { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public UserBalance Balance { get; set; }
    }
}
