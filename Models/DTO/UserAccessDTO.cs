using System.ComponentModel.DataAnnotations;
using static UITraining.Models.GeneralStatus;

namespace UITraining.Models.DTO
{
    public class UserAccessDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        //[Required(ErrorMessage = "Password wajib diisi")]
        public string Password { get; set; }
        //[Required(ErrorMessage = "Konfirmasi Password wajib diisi")]
        //[Compare("Password", ErrorMessage = "Password dan Konfirmasi Password harus sama")]
        public string MatchPassword { get; set; }
        public GeneralStatusData UserStatus { get; set; }
    }
}
