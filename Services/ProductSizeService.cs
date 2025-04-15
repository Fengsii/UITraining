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

        //public List<ProductSizeDTO> GetListProductSize()
        //{
        //    var data = _conteks.ProductSizes.Include(y => y.Product)
        //        .Select(x => new ProductSizeDTO
        //        {
        //            Id = x.Id,
        //            Size = x.Size,
        //            Stock = x.Stock,
        //            ProductName = x.Product.Name,

        //        }).ToList();

        //    return data;
        //}


        //public ProductSize GetProductSizeById(int id)
        //{
        //    var data = _conteks.ProductSizes.FirstOrDefault();
        //    if (data == null)
        //    {
        //        return new ProductSize();
        //    }

        //    return data;
        //}

        //public bool EditProductSize(ProductSizeDTO productSizeDTO)
        //{
        //    var data = _conteks.ProductSizes.FirstOrDefault(x => x.Id == productSizeDTO.Id);
        //    if (data == null)
        //    {
        //        return false;
        //    }
        //    data.Size = productSizeDTO.Size;
        //    data.Stock = productSizeDTO.Stock;
        //    data.ProductId = productSizeDTO.ProductId;
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

        //    _conteks.ProductSizes.Remove(data);
        //    _conteks.SaveChanges();
        //    return true;
        //}

        //public bool AddProdutSize(ProductSizeDTO productSizeDTO)
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



        public List<ProductSizeDTO> GetListProductSize()
        {
            return _conteks.ProductSizes
                .Include(ps => ps.Product)
                .Select(x => new ProductSizeDTO
                {
                    Id = x.Id,
                    Size = x.Size,
                    Stock = x.Stock,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).ToList();
        }

        public ProductSize GetProductSizeById(int id)
        {
            return _conteks.ProductSizes
                .Include(ps => ps.Product)
                .FirstOrDefault(ps => ps.Id == id)
                ?? new ProductSize();
        }

        public bool EditProductSize(ProductSizeDTO productSizeDTO)
        {
            var data = _conteks.ProductSizes.Find(productSizeDTO.Id);
            if (data == null) return false;

            data.Size = productSizeDTO.Size;
            data.Stock = productSizeDTO.Stock;
            data.ProductId = productSizeDTO.ProductId;
            data.UpdatedAt = DateTime.Now;

            _conteks.ProductSizes.Update(data);
            return _conteks.SaveChanges() > 0;
        }

        public bool DeleteProductSize(int id)
        {
            var data = _conteks.ProductSizes.Find(id);
            if (data == null) return false;

            _conteks.ProductSizes.Remove(data);
            return _conteks.SaveChanges() > 0;
        }

        public bool AddProdutSize(ProductSizeDTO productSizeDTO)
        {
            try
            {
                // Validasi dasar
                if (productSizeDTO == null)
                    return false;

                // Validasi ukuran tidak boleh kosong
                if (string.IsNullOrWhiteSpace(productSizeDTO.Size))
                {
                    throw new ArgumentException("Ukuran produk harus diisi");
                }

                // Validasi stok tidak negatif
                if (productSizeDTO.Stock < 0)
                {
                    throw new ArgumentException("Stok tidak boleh negatif");
                }

                // Validasi ProductId harus valid
                if (productSizeDTO.ProductId <= 0)
                {
                    throw new ArgumentException("Produk harus dipilih");
                }

                // Cek apakah product ada di database
                var productExists = _conteks.Product2s.Any(p => p.Id == productSizeDTO.ProductId);
                if (!productExists)
                {
                    throw new ArgumentException("Produk tidak ditemukan");
                }

                // Buat objek baru
                var newSize = new ProductSize
                {
                    Size = productSizeDTO.Size.Trim(),
                    Stock = productSizeDTO.Stock,
                    ProductId = productSizeDTO.ProductId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                // Tambahkan ke database
                _conteks.ProductSizes.Add(newSize);

                // Simpan perubahan
                var result = _conteks.SaveChanges() > 0;

                return result;
            }
            catch (DbUpdateException dbEx)
            {
                // Log error database
                Console.WriteLine($"Database error: {dbEx.Message}");
                throw new Exception("Gagal menyimpan data ke database");
            }
            catch (Exception ex)
            {
                // Log error umum
                Console.WriteLine($"Error: {ex.Message}");
                throw; // Re-throw exception untuk ditangani di controller
            }
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
