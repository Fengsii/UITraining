using UITraining.Models.DB;

namespace UITraining.Interfaces
{
    public interface IProduct
    {
        public List<Product> Getlistproduct();

        //BARU DITAMBAHKAN TGL 23 SETELAH INI BARU KE CONTROLLER

        public Product GetProductById(int id);

        public bool EditProduct(Product product);
        public bool DeleteProduct(int id);
    }
}
