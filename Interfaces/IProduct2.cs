using Microsoft.AspNetCore.Mvc.Rendering;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Interfaces
{
    public interface IProduct2
    {
        public List<ProductDTO2> GetlistProduct2();
        public Product2 GetProduct2ById(int id);
        public bool EditProduct2(ProductDTO2 productDTO2);
        public bool DeleteProduct2(int id);
        public bool AddProduct2(ProductDTO2 productDTO2);
        public List<SelectListItem> Product2s();
    }
}
