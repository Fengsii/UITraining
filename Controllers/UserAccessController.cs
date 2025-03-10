using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class UserAccessController : Controller
    {
        private readonly IUserAccess _IUserAccess;
        public UserAccessController(IUserAccess userAccess)
        {
            _IUserAccess = userAccess;
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


        [HttpPost]
        public IActionResult RegisterUser(UserAccessDTO userAccessDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(userAccessDTO); // Jika validasi gagal, kembalikan ke form dengan pesan error
            }

            if (userAccessDTO.Password != userAccessDTO.MatchPassword)
            {
                ModelState.AddModelError("MatchPassword", "Password dan Konfirmasi Password harus sama!");
                return View(userAccessDTO);
            }

            try
            {
                var isSuccess = _IUserAccess.InsertUserAccess(userAccessDTO);
                if (isSuccess)
                {
                    return RedirectToAction("Index", "Dashboard"); // Redirect jika berhasil
                }

                ModelState.AddModelError("", "Gagal mendaftarkan user. Silakan coba lagi.");
                return View(userAccessDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Terjadi kesalahan saat mendaftarkan user.");
                return View(userAccessDTO);
            }
        }



    }
}
