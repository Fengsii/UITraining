namespace UITraining.Models.DB
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }

    public enum ProductStatus{
        published,// semua bisa lihat
        unpublished,//admin aja yang lihat
        delete//cuman ad didata base
    }


}
