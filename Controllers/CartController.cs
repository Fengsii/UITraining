using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _cart;
        private readonly IAuthentication _authentication;
        private readonly IProduct2 _product;

        public CartController(ICart cart, IAuthentication authentication, IProduct2 product)
        {
            _cart = cart;
            _authentication = authentication;
            _product = product;
        }


        // GET: ProductController
        public ActionResult Index()
        {
            var data = _cart.GetlistCart();
            return View(data);
        }



        public IActionResult Edit(int id)
        {
            ViewBag.User = _authentication.Users();
            ViewBag.Product = _product.Product2s();
            var data = _cart.GetCartById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(CartDTO cartDTO)
        {
            if (cartDTO.Id == 0)
            {
                var data = _cart.AddCart(cartDTO);
                if (data)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                var data = _cart.EditCart(cartDTO);
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
            var data = _cart.DeleteCart(id);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Gagal menghapus Cart.");
        }
    }
}
