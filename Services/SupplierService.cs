using Microsoft.AspNetCore.Mvc.Rendering;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;

namespace UITraining.Services
{
    public class SupplierService : ISupplier
    {
        private readonly ApplicationContext _conteks;

       
        public SupplierService(ApplicationContext conteks)
        {
            _conteks = conteks;
        }

        //public List<Supplier> GetlistSupplier()
        //{
        //    //var data = _conteks.Suppliers.Where(x => x.ProductStatus != GeneralStatusData.delete).Select(x => new Product
        //    var data = _conteks.Suppliers.Select(x => new Supplier
        //    {
        //        Id = x.Id,
        //        NameSupplier = x.NameSupplier,
        //        SupplierAddress = x.SupplierAddress,

        //    }).ToList();
        //    return data;

        //}

        public List<SelectListItem> Suppliers()
        {
            var datas = _conteks.Suppliers
                .Select(x => new SelectListItem
                {
                    Text = x.NameSupplier,
                    Value = x.Id.ToString()
                }).ToList();

            return datas;
        }


    }
}
