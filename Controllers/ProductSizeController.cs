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
            var data = _productSize.GetListProductSize();
            return View(data);
        }



        public IActionResult Edit(int id)
        {
            ViewBag.Product2s = _product2.Product2s();
            var data = _productSize.GetProductSizeById(id);
            return View(data);
        }

        //[HttpPost]
        //public IActionResult Edit(ProductSizeDTO productSizeDTO)
        //{
        //    if (productSizeDTO.Id == 0)
        //    {
        //        var data = _productSize.AddProdutSize(productSizeDTO);
        //        if (data)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }

        //    }
        //    else
        //    {
        //        var data = _productSize.EditProductSize(productSizeDTO);
        //        if (data)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }

        //    }
        //    return View();

        //}



        [HttpPost]
        public IActionResult Edit(ProductSizeDTO productSizeDTO)
        {
            try
            {
                // Validasi ModelState
                if (!ModelState.IsValid)
                {
                    ViewBag.Product2s = _product2.Product2s();
                    return View(productSizeDTO);
                }

                bool result;

                // Jika ID = 0, berarti operasi Add
                if (productSizeDTO.Id == 0)
                {
                    result = _productSize.AddProdutSize(productSizeDTO);
                }
                else // Jika ID > 0, berarti operasi Edit
                {
                    result = _productSize.EditProductSize(productSizeDTO);
                }

                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Product2s = _product2.Product2s();
                    ModelState.AddModelError("", "Gagal menyimpan ukuran produk");
                    return View(productSizeDTO);
                }
            }
            catch (ArgumentException argEx)
            {
                // Tangani error validasi
                ViewBag.Product2s = _product2.Product2s();
                ModelState.AddModelError("", argEx.Message);
                return View(productSizeDTO);
            }
            catch (Exception ex)
            {
                // Tangani error umum
                ViewBag.Product2s = _product2.Product2s();
                ModelState.AddModelError("", $"Terjadi kesalahan: {ex.Message}");
                return View(productSizeDTO);
            }
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
