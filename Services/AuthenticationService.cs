using Microsoft.AspNetCore.Identity;
using UITraining.Models.DB;
using UITraining.Models;
using Microsoft.EntityFrameworkCore;
using UITraining.Helper;
using UITraining.Models.DTO;
using UITraining.Interfaces;
using static UITraining.Models.GeneralStatus;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UITraining.Services
{
    public class AuthenticationService : IAuthentication
    {
        private readonly ApplicationContext _conteks;
        private readonly string _paper;
        private readonly string _iteration;

        public AuthenticationService(ApplicationContext conteks, IConfiguration configuration)
        {
            _paper = configuration.GetSection("Security:Papper").Value ?? "";
            _iteration = configuration.GetSection("Security:Iteration").Value ?? "";
            _conteks = conteks;
        }


        public async Task<bool> Register(RegisterDTO registerDTO)
        {
            try
            {
                // Cek apakah username/email sudah dipakai
                if (await _conteks.Users.AnyAsync(u => u.Username == registerDTO.Username || u.Email == registerDTO.Email))
                    return false;

                var salt = Hasher.GenerateSalt();

                var user = new User
                {
                    Name = registerDTO.Name,
                    Username = registerDTO.Username,
                    Email = registerDTO.Email,
                    Salt = salt,
                    PasswordHash = Hasher.ComputeHash(registerDTO.Password, salt, _paper, Convert.ToInt32(_iteration)),
                    Role = "User", // Default role
                    CreatedAt = DateTime.UtcNow,
                    UserStatus = GeneralStatus.GeneralStatusData.Published
                };

                _conteks.Users.Add(user);
                await _conteks.SaveChangesAsync();

                // Buat saldo default
                _conteks.UserBalances.Add(new UserBalance { UserId = user.Id });
                await _conteks.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error di AuthService.Register: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");

                throw; // Lempar kembali exception untuk ditangani pemanggil
            }
        }






        public async Task<(bool Success, string Role, int UserId)> Login(LoginDTO loginDTO)
        {
            // Cari user by username
            var user = await _conteks.Users
                .FirstOrDefaultAsync(u =>
                    u.Username == loginDTO.Username &&
                    u.UserStatus == GeneralStatus.GeneralStatusData.Published);

            if (user == null)
            {
                return (false, null, 0); // Return tuple dengan nilai default
            }

          
            var hashResult = Hasher.ComputeHash(loginDTO.Password, user.Salt, _paper, Convert.ToInt32(_iteration));
            // Bandingkan dengan hash di database
            if (hashResult == user.PasswordHash && loginDTO.Username == user.Username)
            {
                return (true, user.Role, user.Id); // Return tuple dengan data lengkap
            }
            else
            {
                return (false, null, 0);
            }
        }

        public List<RegisterDTO> GetAllUser()
        {
            var data = _conteks.Users.Where(x => x.UserStatus != GeneralStatusData.delete).Select(x => new RegisterDTO
            {
                Id = x.Id,
                Name = x.Name,
                Username = x.Username,
                Email = x.Email,
                Password = "*******",
                ConfirmPassword = "*******",
                UserStatus = x.UserStatus

            }).ToList();
            return data;
        }

        public User GetUserById(int id)
        {
            var data = _conteks.Users.Where(x => x.Id == id && x.UserStatus != GeneralStatusData.delete).FirstOrDefault();
            if (data == null)
            {
                return new User();
            }

            return data;
        }

        public bool EditUser(RegisterDTO registerDTO)
        {
            var data = _conteks.Users.FirstOrDefault(x => x.Id == registerDTO.Id);
            if (data == null)
            {
                return false;
            }

            data.CreatedAt = DateTime.Now;
            data.UserStatus = registerDTO.UserStatus;


            _conteks.Users.Update(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool DeleteUser(int id)
        {
            var data = _conteks.Users.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return false;
            }

            data.UserStatus = GeneralStatusData.delete;
            //_conteks.Products.Update(data);
            _conteks.SaveChanges();
            return true;
        }

        public List<SelectListItem> Users()
        {
            var datas = _conteks.Users
                .Where(x => x.UserStatus == GeneralStatusData.Published)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();


            return datas;
        }


    }
}
