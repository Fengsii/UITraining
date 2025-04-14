using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class ProductSizeController : Controller
    {
        private readonly IProductSize _productSize;
        private readonly IProduct2 _product2;

        public ProductSizeController (IProductSize productSize, IProduct2 product2)
        {
            _productSize = productSize;
            _product2 = product2;
        }


        // GET: ProductController
        public ActionResult Index()
        {
            var data = _productSize.GetlistProductSize();
            return View(data);
        }



        public IActionResult Edit(int id)
        {
            ViewBag.Product2s = _product2.Product2s();
            var data = _productSize.GetProductSizeById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(ProductSizeDTO productSizeDTO)
        {
            if (productSizeDTO.Id == 0)
            {
                var data = _productSize.AddProductSize(productSizeDTO);
                if (data)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                var data = _productSize.EditProductSize(productSizeDTO);
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
            var data = _productSize.DeleteProductSize(id);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Gagal menghapus size.");
        }
    }
}
