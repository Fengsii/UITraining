using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UITraining.Controllers
{
    public class DashboardController : Controller
    {
        // GET: DashboardController
        public ActionResult Index()
        {
            return View();
        }
    }
}
