using Microsoft.AspNetCore.Mvc;
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


        // Handle proses login
        //[HttpPost]
        //public IActionResult Login(UserAccessDTO userAccessDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(userAccessDTO); // Jika validasi gagal, kembalikan ke form login
        //    }

        //    try
        //    {
        //        var isValid = _IUserAccess.ValidateLogin(userAccessDTO.UserName, userAccessDTO.Password);
        //        if (isValid)
        //        {
        //            // Redirect ke dashboard atau halaman lain setelah login berhasil
        //            return RedirectToAction("Index", "Dashboard");
        //        }

        //        // Jika login gagal, tampilkan pesan error
        //        ModelState.AddModelError("", "Username atau Password salah!");
        //        return View(userAccessDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", "Terjadi kesalahan saat login.");
        //        return View(userAccessDTO);
        //    }
        //}


        //[HttpPost]
        //public IActionResult Login(LoginDTO loginDTO) // Ganti parameter menjadi LoginDTO
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(loginDTO); // Kembalikan ke halaman login jika validasi gagal
        //    }

        //    try
        //    {
        //        var isValid = _IUserAccess.ValidateLogin(loginDTO.UserName, loginDTO.Password);
        //        if (isValid)
        //        {
        //            // Redirect ke halaman dashboard atau halaman lain setelah login berhasil
        //            return RedirectToAction("Index", "Dashboard");
        //        }

        //        // Jika login gagal, tampilkan pesan error
        //        ModelState.AddModelError("", "Username atau Password salah!");
        //        return View(loginDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Tangani kesalahan dan tampilkan pesan error
        //        ModelState.AddModelError("", "Terjadi kesalahan saat login.");
        //        return View(loginDTO);
        //    }
        //}

        //[HttpPost]
        //public IActionResult RegisterUser(UserAccessDTO userAccessDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(userAccessDTO); // Jika validasi gagal, kembalikan ke form dengan pesan error
        //    }

        //    if (userAccessDTO.Password != userAccessDTO.MatchPassword)
        //    {
        //        ModelState.AddModelError("MatchPassword", "Password dan Konfirmasi Password harus sama!");
        //        return View(userAccessDTO);
        //    }

        //    try
        //    {
        //        var isSuccess = _IUserAccess.InsertUserAccess(userAccessDTO);
        //        if (isSuccess)
        //        {
        //            return RedirectToAction("Login", "UserAccess"); // Redirect jika berhasil
        //        }

        //        ModelState.AddModelError("", "Gagal mendaftarkan user. Silakan coba lagi.");
        //        return View(userAccessDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", "Terjadi kesalahan saat mendaftarkan user.");
        //        return View(userAccessDTO);
        //    }
        //}




        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                var isValid = _IUserAccess.ValidateLogin(loginDTO.UserName, loginDTO.Password);
                if (isValid)
                {
                    // Redirect ke halaman dashboard atau halaman lain setelah login berhasil
                    return RedirectToAction("Index", "Dashboard");
                }

                // Jika login gagal, tampilkan pesan error
                TempData["ErrorMessage"] = "Username atau Password salah!";
                return View(loginDTO);
            }
            catch (Exception ex)
            {
                // Tangani kesalahan dan tampilkan pesan error
                TempData["ErrorMessage"] = "Terjadi kesalahan saat login.";
                return View(loginDTO);
            }
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
                var isSuccess = _IUserAccess.InsertUserAccess(userAccessDTO);
                if (isSuccess)
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
