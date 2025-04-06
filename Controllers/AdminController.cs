using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models;

namespace UITraining.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAuth _iauth;
        private readonly ApplicationContext _conteks;
        public AdminController(IAuth auth, ApplicationContext context)
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
    }
}
