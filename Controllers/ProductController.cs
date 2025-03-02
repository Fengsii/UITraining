using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProduct _interface;
        private readonly ISupplier _supplier;

        public ProductController(IProduct interfaces, ISupplier supplier)
        {
            _interface = interfaces;
            _supplier = supplier;
        }


        // GET: ProductController
        public ActionResult Index()
        {
           var data = _interface.Getlistproduct();
           return View(data);
        }


        
        public IActionResult Edit(int id)
        {
            ViewBag.Supplier = _supplier.Suppliers();
           var data = _interface.GetProductById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(ProductDTO product)
        {
            if(product.Id == 0)
            {
                var data = _interface.AddProduct(product);
                if (data)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                var data = _interface.EditProduct(product);
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
            var data = _interface.DeleteProduct(id);
            if (data)
            {
                return RedirectToAction(nameof(Index)); 
            }
            return BadRequest("Gagal menghapus produk.");
        }



    }
}
