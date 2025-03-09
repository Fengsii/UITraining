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

        [HttpPost]
        public IActionResult RegisterUser(UserAccessDTO userAccessDTO)
        {
            try
            {
                var isSuccess = _IUserAccess.InsertUserAccess(userAccessDTO);
                if(isSuccess)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                return View();
            }
            catch(Exception ex)
            {
                return View();
            }
           
        }



    }
}
