using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class ListRegisterUserController : Controller
    {
        private readonly IUserAccess _IUserAccess;
        public ListRegisterUserController(IUserAccess userAccess)
        {
            _IUserAccess = userAccess;
        }
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
            return View(data);

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




    }
}
