using Microsoft.AspNetCore.Mvc;

namespace UITraining.Controllers
{
    public class UserHome : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
