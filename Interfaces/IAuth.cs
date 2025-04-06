using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Interfaces
{
    public interface IAuth
    {
        Task<bool> Register(RegisterDTO registerDTO);
        Task<(bool Success, string Role, int UserId)> Login(LoginDTO loginDTO);
        public List<RegisterDTO> GetAllUser();
        public User GetUserById(int id);
        public bool EditUser(RegisterDTO registerDTO);
        public bool DeleteUser(int id);
    }
}
