using System.ComponentModel.DataAnnotations;

namespace UITraining.Models.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Username wajib diisi")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password wajib diisi")]
        public string Password { get; set; }
    }
}
