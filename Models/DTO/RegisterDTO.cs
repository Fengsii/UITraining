using System.ComponentModel.DataAnnotations;
using static UITraining.Models.GeneralStatus;

namespace UITraining.Models.DTO
{
    public class RegisterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public GeneralStatusData UserStatus { get; set; }

    }
}
