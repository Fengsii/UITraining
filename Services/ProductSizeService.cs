using static UITraining.Models.GeneralStatus;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using UITraining.Models;
using Microsoft.EntityFrameworkCore;

namespace UITraining.Services
{
    public class ProductSizeService
    {
        private readonly ApplicationContext _conteks;

        public ProductSizeService (ApplicationContext conteks)
        {
            _conteks = conteks;
        }

        public List<ProductSizeDTO> GetlistProductSize()
        {
            var data = _conteks.ProductSizes.Include(y => y.Product).Select(x => new ProductSizeDTO
            {
                Id = x.Id,
                Size = x.Size,
                Stock = x.Stock,
                ProductName = x.Product.Name

            }).ToList();
            return data;

        }

        public ProductSize GetProductSizeById(int id)
        {
            var data = _conteks.ProductSizes.FirstOrDefault();
            if (data == null)
            {
                return new ProductSize();
            }

            return data;
        }

        public bool EditProductSize(ProductSize productSize)
        {
            var data = _conteks.ProductSizes.FirstOrDefault(x => x.Id == productSize.Id);
            if (data == null)
            {
                return false;
            }
            data.ProductId = productSize.ProductId;
            data.Size = productSize.Size;
            data.Stock = productSize.Stock;

            _conteks.ProductSizes.Update(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool DeleteProductSize(int id)
        {
            var data = _conteks.ProductSizes.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return false;
            }

            //data.ProductStatus = GeneralStatusData.delete;
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

            //var datasup = _conteks.Suppliers.FirstOrDefault(x => x.Id == product.IdSupplier);
            //if(datasup == null || datasup.SupplierStatus != GeneralStatusData.Active)
            //{
            //    return false;
            //}

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
