using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using UITraining.Services;

namespace UITraining.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthentication _iauth;
        private readonly ApplicationContext _conteks;
        public AuthenticationController(IAuthentication auth, ApplicationContext context)
        {
            _iauth = auth;
            _conteks = context;
        }

        public IActionResult LoginUser()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Index()
        {
            var data = _iauth.GetAllUser();
            return View(data);
        }

        public IActionResult EditUser(int id)
        {
            var data = _iauth.GetUserById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult EditUser(RegisterDTO registerDTO)
        {
            var data = _iauth.EditUser(registerDTO);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = _iauth.DeleteUser(id);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Gagal menghapus User.");
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginDTO loginDTO)
        {
            var (success, role, userId) = await _iauth.Login(loginDTO);

            if (success)
            {
                var user = await _conteks.Users.FindAsync(userId);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("UserId", user.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Redirect berdasarkan peran
                if (role == "Admin")
                {
                    return RedirectToAction("Index", "Dashboard"); // Halaman Admin
                }
                else if (role == "User")
                {
                    return RedirectToAction("Index", "UserHome"); // Halaman User
                }
            }

            TempData["ErrorMessage"] = "Username atau Password salah!";
            return View(loginDTO);
        }

  
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (registerDTO.Password.Length < 7)
            {
                TempData["ErrorMessage"] = "Password minimal 7 karakter";
                return View(registerDTO);
            }

            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                TempData["ErrorMessage"] = "Password dan Konfirmasi Password harus sama!";
                return View(registerDTO);
            }

            var existingUser = await _conteks.Users
                .FirstOrDefaultAsync(x => x.Username == registerDTO.Username || x.Email == registerDTO.Email);

            if (existingUser != null)
            {
                TempData["ErrorMessage"] = "Username atau Email sudah digunakan. Silakan pilih yang lain.";
                return View(registerDTO);
            }

            try
            {
                var result = await _iauth.Register(registerDTO);
                if (result)
                {
                    TempData["SuccessMessage"] = "Registrasi berhasil! Silakan login.";
                    return RedirectToAction("LoginUser");
                }

                TempData["ErrorMessage"] = "Gagal mendaftarkan user. Silakan coba lagi.";
                return View(registerDTO);
            }
            catch (Exception ex)
            {
                // Log exception untuk debugging
                Console.WriteLine($"Error registrasi: {ex.Message}");
                // Log juga inner exception jika ada
                if (ex.InnerException != null)
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");

                TempData["ErrorMessage"] = $"Terjadi kesalahan saat mendaftarkan user: {ex.Message}";
                return View(registerDTO);
            }
        }



    }
}
