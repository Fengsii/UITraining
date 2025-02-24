using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DB;

namespace UITraining.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProduct _interface;

        public ProductController(IProduct interfaces)
        {
            _interface = interfaces; 
        }


        // GET: ProductController
        public ActionResult Index()
        {
           var data = _interface.Getlistproduct();
           return View(data);
        }


        
        public IActionResult Edit(int id)
        {
           var data = _interface.GetProductById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var data = _interface.EditProduct(product);
            if(data)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

     
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = _interface.DeleteProduct(id);
            if (data)
            {
                return RedirectToAction(nameof(Index)); 
            }
            return BadRequest("Gagal menghapus produk.");
        }



    }
}
