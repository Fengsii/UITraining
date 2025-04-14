using UITraining.Interfaces;
using static UITraining.Models.GeneralStatus;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using UITraining.Models;
using Microsoft.EntityFrameworkCore;

namespace UITraining.Services
{
    public class ProductSizeService : IProductSize
    {
        private readonly ApplicationContext _conteks;

        public ProductSizeService(ApplicationContext conteks)
        {
            _conteks = conteks;
        }
        public List<ProductSizeDTO> GetlistProductSize()
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

        //public bool EditProductSize(ProductSizeDTO productSizeDTO)
        //{
        //    var data = _conteks.ProductSizes.FirstOrDefault(x => x.Id == productSizeDTO.Id);
        //    if (data == null)
        //    {
        //        return false;
        //    }
        //    data.ProductId = productSizeDTO.ProductId;
        //    data.Size = productSizeDTO.Size;
        //    data.Stock = productSizeDTO.Stock;
        //    data.UpdatedAt = DateTime.Now;

        //    _conteks.ProductSizes.Update(data);
        //    _conteks.SaveChanges();
        //    return true;
        //}

        //public bool DeleteProductSize(int id)
        //{
        //    var data = _conteks.ProductSizes.FirstOrDefault(x => x.Id == id);
        //    if (data == null)
        //    {
        //        return false;
        //    }

        //    //data.ProductStatus = GeneralStatusData.delete;
        //    _conteks.ProductSizes.Remove(data);
        //    _conteks.SaveChanges();
        //    return true;
        //}

        //public bool AddProductSize(ProductSizeDTO productSizeDTO)
        //{
        //    var data = new ProductSize();

        //    data.Size = productSizeDTO.Size;
        //    data.Stock = productSizeDTO.Stock;
        //    data.ProductId = productSizeDTO.ProductId;
        //    data.CreatedAt = DateTime.Now;

        //    _conteks.ProductSizes.Add(data);
        //    _conteks.SaveChanges();
        //    return true;

        //}

        // Menambahkan ukuran produk baru
        //public bool AddProductSize(ProductSizeDTO productSizeDTO)
        //{
        //    var productSize = new ProductSize
        //    {
        //        Size = productSizeDTO.Size,
        //        Stock = productSizeDTO.Stock,
        //        ProductId = productSizeDTO.ProductId,
        //        CreatedAt = DateTime.Now
        //    };

        //    _conteks.ProductSizes.Add(productSize);

        //    var product = _conteks.Products.FirstOrDefault(p => p.Id == productSizeDTO.ProductId);
        //    if (product != null)
        //    {
        //        product.Stock += productSizeDTO.Stock; 
        //        _conteks.Products.Update(product);
        //    }

        //    _conteks.SaveChanges();
        //    return true;
        //}

        public bool AddProductSize(ProductSizeDTO productSizeDTO)
        {
            try
            {
                // Validasi apakah ProductId valid
                var productExists = _conteks.Product2s.Any(p => p.Id == productSizeDTO.ProductId);
                if (!productExists)
                {
                    throw new Exception("Product dengan ID tersebut tidak ditemukan.");
                }

                // Buat entitas ProductSize baru
                var productSize = new ProductSize
                {
                    Size = productSizeDTO.Size,
                    Stock = productSizeDTO.Stock,
                    ProductId = productSizeDTO.ProductId,
                    CreatedAt = DateTime.Now
                };

                // Tambahkan ke database
                _conteks.ProductSizes.Add(productSize);

                // Update total stock di tabel Product
                var product = _conteks.Product2s.FirstOrDefault(p => p.Id == productSizeDTO.ProductId);
                if (product != null)
                {
                    product.Stock += productSizeDTO.Stock; // Tambahkan stok baru ke total stok

                    // Pastikan hanya kolom Stock yang diperbarui
                    _conteks.Entry(product).Property(p => p.Stock).IsModified = true;
                }

                // Simpan perubahan
                _conteks.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                throw; // Lemparkan kembali error jika diperlukan
            }
        }

        public bool EditProductSize(ProductSizeDTO productSizeDTO)
        {
            var productSize = _conteks.ProductSizes.FirstOrDefault(ps => ps.Id == productSizeDTO.Id);
            if (productSize == null)
            {
                return false;
            }

            var stockDifference = productSizeDTO.Stock - productSize.Stock;

            productSize.Size = productSizeDTO.Size;
            productSize.Stock = productSizeDTO.Stock;
            productSize.UpdatedAt = DateTime.Now;

            var product = _conteks.Products.FirstOrDefault(p => p.Id == productSize.ProductId);
            if (product != null)
            {
                product.Stock += stockDifference; 
                _conteks.Products.Update(product);
            }

            _conteks.ProductSizes.Update(productSize);
            _conteks.SaveChanges();

            return true;
        }

        public bool DeleteProductSize(int id)
        {
            var productSize = _conteks.ProductSizes.FirstOrDefault(ps => ps.Id == id);
            if (productSize == null) return false;

            var product = _conteks.Products.FirstOrDefault(p => p.Id == productSize.ProductId);
            if (product != null)
            {
                product.Stock -= productSize.Stock;
                _conteks.Products.Update(product);
            }

            _conteks.ProductSizes.Remove(productSize);
            _conteks.SaveChanges();

            return true;
        }


    }
}
