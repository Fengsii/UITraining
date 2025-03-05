using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class SupplierController : Controller
    {

        private readonly ISupplier _supplier;
        public SupplierController(ISupplier supplier)
        {
              _supplier = supplier;
        }

        public IActionResult Index()
        {
            var data = _supplier.GetlistSupplier();
            return View(data);
        }

        public IActionResult EditSupplier(int id)
        {
            var data = _supplier.GetSupplierById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult EditSupplier(SupplierDTO supplier)
        {
            if (supplier.Id == 0)
            {
                var data = _supplier.AddSupplier(supplier);
                if (data)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                var data = _supplier.EditSuppliers(supplier);
                if (data)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View();

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = _supplier.DeleteSupplier(id);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Gagal menghapus supplier.");
        }


    }
}
