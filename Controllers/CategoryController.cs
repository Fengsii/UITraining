using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICatagory _catagory;

        public CategoryController(ICatagory catagory)
        {
            _catagory = catagory;
        }
        public IActionResult Index()
        {
            var data = _catagory.GetListCategory();
            return View(data);
        }

        public IActionResult Edit(int id)
        {
            var data = _catagory.GetCategoryById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(CategoryDTO categoryDTO)
        {
            if (categoryDTO.Id == 0)
            {
                var data = _catagory.AddCategory(categoryDTO);
                if (data)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                var data = _catagory.EditCategory(categoryDTO);
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
            var data = _catagory.DeleteCategory(id);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Gagal menghapus category.");
        }

    }
}
