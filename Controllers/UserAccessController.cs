using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class UserAccessController : Controller
    {
        private readonly IUserAccess _IUserAccess;
        private readonly ApplicationContext _conteks;
        public UserAccessController(IUserAccess userAccess, ApplicationContext context)
        {
            _IUserAccess = userAccess;
            _conteks = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult RegisterUser()
        {
            return View();
        }

        // Baru Ditambahkan Untul User List \\


        public IActionResult Index()
        {
            var data = _IUserAccess.GetlistUser();
            return View(data);
        }

        public IActionResult EditUser(int id)
        {
            var data = _IUserAccess.GetUserById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult EditUser(UserAccessDTO userAccessDTO)
        {
            var data = _IUserAccess.EditUser(userAccessDTO);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = _IUserAccess.DeleteUser(id);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Gagal menghapus User.");
        }

        // ============== BATASAN  ============== \\

        //[HttpPost]
        //public IActionResult RegisterUser(UserAccessDTO userAccessDTO)
        //{
        //    try
        //    {
        //        var isSuccess = _IUserAccess.InsertUserAccess(userAccessDTO);
        //        if(isSuccess)
        //        {
        //            return RedirectToAction("Index", "Dashboard");
        //        }
        //        return View();
        //    }
        //    catch(Exception ex)
        //    {
        //        return View();
        //    }

        //}



        //[HttpPost]
        //public IActionResult Login(UserAccessDTO loginDTO)
        //{
        //    try
        //    {
        //        var datauser = _IUserAccess.ValidateLogin(loginDTO.UserName, loginDTO.Password);
        //        if (datauser)
        //        {
        //            return RedirectToAction("Index", "Dashboard");
        //        }

        //        TempData["ErrorMessage"] = "Username atau Password salah!";
        //        return View(loginDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = "Terjadi kesalahan saat login.";
        //        return View(loginDTO);
        //    }
        //}





        [HttpPost]
        public async Task<IActionResult> Login(UserAccessDTO loginDTO)
        {
            var datauser = _IUserAccess.ValidateLogin(loginDTO.UserName, loginDTO.Password);
            if (datauser)
            {
                var user = _conteks.UserAccesses.FirstOrDefault(x => x.UserName == loginDTO.UserName);

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim("UserId", user.Id.ToString())
            };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Dashboard");
            }

            TempData["ErrorMessage"] = "Username atau Password salah!";
            return View(loginDTO);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "UserAccess");
        }







        [HttpPost]
        public IActionResult RegisterUser(UserAccessDTO userAccessDTO)
        {
            if(userAccessDTO.Password.Length < 7)
            {
                TempData["ErrorMessage"] = "Password minimal 7 karakter";
                return View(userAccessDTO);
            }

            if (userAccessDTO.Password != userAccessDTO.MatchPassword)
            {
                TempData["ErrorMessage"] = "Password dan Konfirmasi Password harus sama!";
                return View(userAccessDTO);
            }

            var data = _conteks.UserAccesses
                .FirstOrDefault(x => x.UserName == userAccessDTO.UserName);
            if (data != null)
            {
                TempData["ErrorMessage"] = "Username sudah digunakan. Silakan pilih username lain.";
                return View(userAccessDTO);
            }

            try
            {
                var datauser = _IUserAccess.InsertUserAccess(userAccessDTO);
                if (datauser)
                {
                    TempData["SuccessMessage"] = "Registrasi berhasil! Silakan login."; // Pesan sukses
                    return RedirectToAction("Login", "UserAccess"); // Redirect jika berhasil
                }

                TempData["ErrorMessage"] = "Gagal mendaftarkan user. Silakan coba lagi.";
                return View(userAccessDTO);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Terjadi kesalahan saat mendaftarkan user.";
                return View(userAccessDTO);
            }
        }



    }
}
