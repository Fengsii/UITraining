using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class Product2Controller : Controller
    {
        private readonly IProduct2 _interface2;
        private readonly ICatagory _catagory;

        public Product2Controller(IProduct2 interface2, ICatagory catagory)
        {
            _interface2 = interface2;
            _catagory = catagory;
        }


        // GET: ProductController
        public ActionResult Index()
        {
            var data = _interface2.GetListProduct2();
            return View(data);
        }



        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _catagory.Categories();
            var data = _interface2.GetProduct2ById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(ProductDTO2 productDTO2)
        {
            if (productDTO2.Id == 0)
            {
                var data = _interface2.AddProduct2(productDTO2);
                if (data)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                var data = _interface2.EditProduct2(productDTO2);
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
            var data = _interface2.DeleteProduct2(id);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Gagal menghapus produk.");
        }
    }
}
