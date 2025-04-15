using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Interfaces
{
    public interface IProductSize
    {
        public List<ProductSizeDTO> GetlistProductSize();
        public ProductSize GetProductSizeById(int id);
        public bool EditProductSize(ProductSizeDTO productSizeDTO);
        public bool DeleteProductSize(int id);
        public bool AddProduct(ProductSizeDTO productSize);
    }
}
