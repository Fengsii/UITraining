using Microsoft.AspNetCore.Mvc.Rendering;
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
        public ProductSizeService (ApplicationContext context)
        {
            _conteks = context;
        }

        public List<ProductSizeDTO> GetListProductSize()
        {
            var data = _conteks.ProductSizes.Include(y => y.Product)
                .Select(x => new ProductSizeDTO
                {
                    Id = x.Id,
                    Size = x.Size,
                    Stock = x.Stock,
                    ProductName = x.Product.Name,
                   
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

        public bool EditProductSize(ProductSizeDTO productSizeDTO)
        {
            var data = _conteks.ProductSizes.FirstOrDefault(x => x.Id == productSizeDTO.Id);
            if (data == null)
            {
                return false;
            }
            data.Size = productSizeDTO.Size;
            data.Stock = productSizeDTO.Stock;
            data.ProductId = productSizeDTO.ProductId;
            data.UpdatedAt = DateTime.Now;

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

            _conteks.ProductSizes.Remove(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool AddProdutSize(ProductSizeDTO productSizeDTO)
        {

            var data = new ProductSize();

            data.Size = productSizeDTO.Size;
            data.Stock = productSizeDTO.Stock;
            data.ProductId = productSizeDTO.ProductId;
            data.CreatedAt = DateTime.Now;

            _conteks.ProductSizes.Add(data);
            _conteks.SaveChanges();
            return true;

        }

      
       

        //public List<SelectListItem> Product2s()
        //{
        //    var datas = _conteks.Product2s
        //        .Where(x => x.ProductStatus == GeneralStatusData.Published)
        //        .Select(x => new SelectListItem
        //        {
        //            Text = x.Name,
        //            Value = x.Id.ToString()
        //        }).ToList();


        //    return datas;
        //}
    }
}
