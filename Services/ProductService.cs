using UITraining.Models.DB;
using UITraining.Models;
using UITraining.Interfaces;

namespace UITraining.Services
{
    public class ProductService : IProduct
    {
        private readonly ApplicationContext _conteks;

        public ProductService(ApplicationContext conteks)
        {
            _conteks = conteks;
        }

        public List<Product> Getlistproduct()
        {
            var data = _conteks.Products.Where(x => x.ProductStatus != ProductStatus.delete).Select(x => new Product
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                ProductStatus = x.ProductStatus,
               
            }).ToList();
            return data;

        }

        public Product GetProductById(int id)
        {
            var data = _conteks.Products.Where(x => x.Id == id && x.ProductStatus != ProductStatus.delete).FirstOrDefault();
            if(data == null)
            {
                return new Product();
            }

            return data;
        }

        public bool EditProduct(Product product)
        {
            var data = _conteks.Products.FirstOrDefault(x => x.Id == product.Id);
            if (data == null)
            {
                return false;
            }
            data.Name = product.Name;
            data.Description = product.Description;
            data.Price = product.Price;
            data.Stock = product.Stock;
            data.ProductStatus = product.ProductStatus;

            _conteks.Products.Update(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool DeleteProduct(int id)
        {
            var data = _conteks.Products.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return false; 
            }

            data.ProductStatus = ProductStatus.delete;
            _conteks.Products.Update(data);
            _conteks.SaveChanges();
            return true;
        }



    }
}
