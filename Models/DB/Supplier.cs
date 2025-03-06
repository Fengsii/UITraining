using static UITraining.Models.GeneralStatus;

namespace UITraining.Models.DB
{
    public class Supplier
    {
        public int Id { get; set; }
        public string NameSupplier { get; set; }
        public string SupplierAddress { get; set; }
        public GeneralStatusData SupplierStatus { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
