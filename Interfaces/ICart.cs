using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Interfaces
{
    public interface ICart
    {
        public List<CartDTO> GetlistCart();
        public Cart GetCartById(int id);
        public bool EditCart(CartDTO cartDTO);
        public bool DeleteCart(int id);
        public bool AddCart(CartDTO cartDTO);
    }
}
