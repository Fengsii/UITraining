using Microsoft.AspNetCore.Mvc.Rendering;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Interfaces
{
    public interface ISupplier
    {
        public List<SupplierDTO> GetlistSupplier();
        public Supplier GetSupplierById(int id);
        public bool EditSuppliers(SupplierDTO supplier);
        public bool AddSupplier(SupplierDTO supplier);
        public bool DeleteSupplier(int id);
        public List<SelectListItem> Suppliers();
    }
}
