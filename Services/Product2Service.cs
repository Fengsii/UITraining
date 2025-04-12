using UITraining.Interfaces;
using static UITraining.Models.GeneralStatus;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using UITraining.Models;
using Microsoft.EntityFrameworkCore;

namespace UITraining.Services
{
    public class Product2Service : IProduct2
    {
        private readonly ApplicationContext _conteks;

        public Product2Service(ApplicationContext conteks)
        {
            _conteks = conteks;
        }
        public List<ProductDTO2> GetlistProduct2()
        {
            var data = _conteks.Product2s.Include(y => y.Category)
                .Where(x => x.ProductStatus != GeneralStatus.GeneralStatusData.delete)
                .Select(x => new ProductDTO2
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Image = x.Image,
                    Stock = x.Stock,
                    IsPromo = x.IsPromo,
                    Discount = x.Discount,
                    ProductStatus = x.ProductStatus,
                    CategoryName = x.Category.CategoryName
                }).ToList();

            return data;
        }


        public Product2 GetProduct2ById(int id)
        {
            var data = _conteks.Product2s.Where(x => x.Id == id && x.ProductStatus != GeneralStatusData.delete).FirstOrDefault();
            if (data == null)
            {
                return new Product2();
            }

            return data;
        }

        public bool EditProduct2(ProductDTO2 productDTO2)
        {
            var data = _conteks.Product2s.FirstOrDefault(x => x.Id == productDTO2.Id);
            if (data == null)
            {
                return false;
            }
            data.CategoryId = productDTO2.CategoryId;
            data.Name = productDTO2.Name;
            data.Description = productDTO2.Description;
            data.Price = productDTO2.Price;
            data.Image = productDTO2.Image;
            data.Stock = productDTO2.Stock;
            data.IsPromo = productDTO2.IsPromo;
            data.Discount = productDTO2.Discount;
            data.ProductStatus = productDTO2.ProductStatus;

            _conteks.Product2s.Update(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool DeleteProduct2(int id)
        {
            var data = _conteks.Product2s.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return false;
            }

            data.ProductStatus = GeneralStatusData.delete;
            _conteks.SaveChanges();
            return true;
        }

        public bool AddProduct2(ProductDTO2 productDTO2)
        {
            var data = new Product2();

            data.Name = productDTO2.Name;
            data.Description = productDTO2.Description;
            data.Price = productDTO2.Price;
            data.Image = productDTO2.Image;
            data.Stock = productDTO2.Stock;
            data.IsPromo = productDTO2.IsPromo;
            data.Discount = productDTO2.Discount;
            data.ProductStatus = productDTO2.ProductStatus;
            data.CategoryId = productDTO2.CategoryId;


            _conteks.Product2s.Add(data);
            _conteks.SaveChanges();
            return true;

        }
    }
}
