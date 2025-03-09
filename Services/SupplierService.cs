using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using static UITraining.Models.GeneralStatus;

namespace UITraining.Services
{
    public class SupplierService : ISupplier
    {
        private readonly ApplicationContext _conteks;

       
        public SupplierService(ApplicationContext conteks)
        {
            _conteks = conteks;
        }


        public List<SupplierDTO> GetlistSupplier()
        {
            var data = _conteks.Suppliers.Where(x => x.SupplierStatus != GeneralStatusData.delete).Select(x => new SupplierDTO
            {
                Id = x.Id,
                NameSupplier = x.NameSupplier,
                SupplierAddress = x.SupplierAddress,
                SupplierStatus = x.SupplierStatus,

            }).ToList();
            return data;

        }

        public Supplier GetSupplierById(int id)
        {
            var data = _conteks.Suppliers.Where(x => x.Id == id && x.SupplierStatus != GeneralStatusData.delete).FirstOrDefault();
            if (data == null)
            {
                return new Supplier();
            }

            return data;
        }


        public bool EditSuppliers(SupplierDTO supplier)
        {
            var data = _conteks.Suppliers.FirstOrDefault(x => x.Id == supplier.Id);
            if (data == null)
            {
                return false;
            }
            data.Id = supplier.Id;
            data.NameSupplier = supplier.NameSupplier;
            data.SupplierAddress = supplier.SupplierAddress;
            data.SupplierStatus = supplier.SupplierStatus;


            _conteks.Suppliers.Update(data);
            _conteks.SaveChanges();
            return true;
        }


        public bool AddSupplier(SupplierDTO supplier)
        {
            //var data = _conteks.Products.Select(x => new Product
            //{
            //    IdSupplier = product.IdSupplier,
            //    Name = product.Name,
            //    Description = product.Description,
            //    Price = product.Price,
            //    ProductStatus = product.ProductStatus.
            //});


            var data = new Supplier();
            data.NameSupplier = supplier.NameSupplier;
            data.SupplierAddress = supplier.SupplierAddress;
            data.SupplierStatus = supplier.SupplierStatus;

            _conteks.Suppliers.Add(data);
            _conteks.SaveChanges();
            return true;

        }

        public bool DeleteSupplier(int id)
        {
            var data = _conteks.Suppliers.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return false;
            }

            //// Ambil semua produk yang terkait dengan supplier ini dan tandai sebagai dihapus
            //var relatedProducts = _conteks.Products.Where(p => p.IdSupplier == id).ToList();
            //foreach (var product in relatedProducts)
            //{
            //    product.ProductStatus = GeneralStatusData.delete;
            //}

            //// Tandai supplier sebagai dihapus
            //data.SupplierStatus = GeneralStatusData.delete;


            data.SupplierStatus = GeneralStatusData.delete;
            //_conteks.Products.Update(data);
            _conteks.SaveChanges();
            return true;
        }



        public List<SelectListItem> Suppliers()
        {
            var datas = _conteks.Suppliers
                .Where(x => x.SupplierStatus == GeneralStatusData.Published)
                .Select(x => new SelectListItem
                {
                    Text = x.NameSupplier,
                    Value = x.Id.ToString()
                }).ToList();


            return datas;
        }


    }
}
