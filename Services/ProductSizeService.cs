using static UITraining.Models.GeneralStatus;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using UITraining.Models;
using Microsoft.EntityFrameworkCore;
using UITraining.Interfaces;

namespace UITraining.Services
{
    public class ProductSizeService : IProductSize
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
            var data = _conteks.ProductSizes.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return new ProductSize();
            }
            return data;
        }

        public bool EditProductSize(ProductSizeDTO productSizeDTO)
        {
            var data = _conteks.ProductSizes.FirstOrDefault(x => x.Id == productSizeDTO.Id);
            if (data == null)
            {
                return false;
            }
            data.ProductId = productSizeDTO.ProductId;
            data.Size = productSizeDTO.Size;
            data.Stock = productSizeDTO.Stock;

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
            _conteks.ProductSizes.Remove(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool AddProduct(ProductSizeDTO productSize)
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

            var data = new ProductSize();

            data.Size = productSize.Size;
            data.Stock = productSize.Stock;
            data.ProductId = productSize.ProductId;


            _conteks.ProductSizes.Add(data);
            _conteks.SaveChanges();
            return true;

        }
    }
}
