using Microsoft.AspNetCore.Mvc.Rendering;
using UITraining.Models.DB;

namespace UITraining.Interfaces
{
    public interface ISupplier
    {
        //public List<Supplier> GetlistSupplier();

        public List<SelectListItem> Suppliers();
    }
}
