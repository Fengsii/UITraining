using Microsoft.AspNetCore.Mvc.Rendering;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Interfaces
{
    public interface ICatagory
    {
        public List<CategoryDTO> GetListCategory();
        public Category GetCategoryById(int id);
        public bool EditCategory(CategoryDTO categoryDTO);
        public bool DeleteCategory(int id);
        public bool AddCategory(CategoryDTO categoryDTO);
        public List<SelectListItem> Categories();
    }
}
