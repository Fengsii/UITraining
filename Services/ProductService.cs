using UITraining.Models.DB;
using UITraining.Models;
using UITraining.Interfaces;
using static UITraining.Models.GeneralStatus;
using Microsoft.EntityFrameworkCore;
using UITraining.Models.DTO;

namespace UITraining.Services
{
    public class ProductService : IProduct
    {
        private readonly ApplicationContext _conteks;

        public ProductService(ApplicationContext conteks)
        {
            _conteks = conteks;
        }

        public List<ProductDTO> Getlistproduct()
        {
            var data = _conteks.Products.Include(y => y.Supplier).Where(x => x.ProductStatus != GeneralStatusData.delete).Select(x => new ProductDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                ProductStatus = x.ProductStatus,
                SupplierName = x.Supplier.NameSupplier
               
            }).ToList();
            return data;

        }

        public Product GetProductById(int id)
        {
            var data = _conteks.Products.Where(x => x.Id == id && x.ProductStatus != GeneralStatusData.delete).FirstOrDefault();
            if(data == null)
            {
                return new Product();
            }

            return data;
        }

        public bool EditProduct(ProductDTO product)
        {
            var data = _conteks.Products.FirstOrDefault(x => x.Id == product.Id);
            if (data == null)
            {
                return false;
            }
            data.IdSupplier = product.IdSupplier;
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

            data.ProductStatus = GeneralStatusData.delete;
            //_conteks.Products.Update(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool AddProduct(ProductDTO product)
        {
            //var data = _conteks.Products.Select(x => new Product
            //{
            //    IdSupplier = product.IdSupplier,
            //    Name = product.Name,
            //    Description = product.Description,
            //    Price = product.Price,
            //    ProductStatus = product.ProductStatus.
            //});

            var datasup = _conteks.Suppliers.FirstOrDefault(x => x.Id == product.IdSupplier);
            if (datasup == null || datasup.SupplierStatus != GeneralStatusData.Active)
            {
                return false;
            }

            var data = new Product();

                data.Name = product.Name;
                data.Description = product.Description;
                data.Price = product.Price;

                data.Stock = product.Stock;
                data.ProductStatus = product.ProductStatus;
                data.IdSupplier = product.IdSupplier;


            _conteks.Products.Add(data);
            _conteks.SaveChanges();
            return true;

        }



    }
}
