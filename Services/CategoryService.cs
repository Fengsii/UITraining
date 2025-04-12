using Microsoft.EntityFrameworkCore;
using UITraining.Models;
using static UITraining.Models.GeneralStatus;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using UITraining.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UITraining.Services
{
    public class CategoryService : ICatagory
    {
        private readonly ApplicationContext _conteks;
        public CategoryService(ApplicationContext context)
        {
            _conteks = context;
        }

        public List<CategoryDTO> GetListCategory()
        {
            var data = _conteks.Categories.Where(x => x.CategoryStatus != GeneralStatus.GeneralStatusData.delete)
                .Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    CategoryStatus = x.CategoryStatus
                }).ToList();

            return data;
        }


        public Category GetCategoryById(int id)
        {
            var data = _conteks.Categories.Where(x => x.Id == id && x.CategoryStatus != GeneralStatusData.delete).FirstOrDefault();
            if (data == null)
            {
                return new Category();
            }

            return data;
        }

        public bool EditCategory(CategoryDTO categoryDTO)
        {
            var data = _conteks.Categories.FirstOrDefault(x => x.Id == categoryDTO.Id);
            if (data == null)
            {
                return false;
            }
            data.Id = categoryDTO.Id;
            data.CategoryName = categoryDTO.CategoryName;
            data.Description = categoryDTO.Description;
            data.CategoryStatus = categoryDTO.CategoryStatus;
            data.UpdatedAt = DateTime.Now;

            _conteks.Categories.Update(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool DeleteCategory(int id)
        {
            var data = _conteks.Categories.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return false;
            }

            data.CategoryStatus = GeneralStatusData.delete;
            _conteks.SaveChanges();
            return true;
        }

        public bool AddCategory(CategoryDTO categoryDTO)
        {
            
            var data = new Category();

            data.CategoryName = categoryDTO.CategoryName;
            data.Description = categoryDTO.Description;
            data.CategoryStatus = categoryDTO.CategoryStatus;
            data.CreatedAt = DateTime.Now;

            _conteks.Categories.Add(data);
            _conteks.SaveChanges();
            return true;

        }

        public List<SelectListItem> Categories()
        {
            var datas = _conteks.Categories
                .Where(x => x.CategoryStatus == GeneralStatusData.Published)
                .Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                }).ToList();


            return datas;
        }


    }
}
