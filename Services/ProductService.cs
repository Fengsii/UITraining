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
               
            }).ToList();
            return data;



        }

    }
}
