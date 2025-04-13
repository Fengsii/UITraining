using UITraining.Interfaces;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using UITraining.Models;
using Microsoft.EntityFrameworkCore;

namespace UITraining.Services
{
    public class CartService : ICart
    {
        private readonly ApplicationContext _conteks;

        public CartService(ApplicationContext conteks)
        {
            _conteks = conteks;
        }
        public List<CartDTO> GetlistCart()
        {
            var data = _conteks.Carts.Include(y => y.User).Include(w => w.Product)
                .Select(x => new CartDTO
                {
                    Id = x.Id,
                    UserName = x.User.Username,
                    ProductName = x.Product.Name,
                    Image = x.Image,
                    Quantity = x.Quantity,
                    SelectedSize = x.SelectedSize,

                }).ToList();

            return data;
        }


        public Cart GetCartById(int id)
        {
            var data = _conteks.Carts.FirstOrDefault();
            if (data == null)
            {
                return new Cart();
            }

            return data;
        }

        public bool EditCart(CartDTO cartDTO)
        {
            var data = _conteks.Carts.FirstOrDefault(x => x.Id == cartDTO.Id);
            if (data == null)
            {
                return false;
            }

            data.UserId = cartDTO.UserId;
            data.ProductId = cartDTO.ProductId;
            data.Image = cartDTO.Image;
            data.Quantity = cartDTO.Quantity;
            data.SelectedSize = cartDTO.SelectedSize;

            _conteks.Carts.Update(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool DeleteCart(int id)
        {
            var data = _conteks.Carts.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return false;
            }

            //data.ProductStatus = GeneralStatusData.delete;
            _conteks.Carts.Remove(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool AddCart(CartDTO cartDTO)
        {
            var data = new Cart();

            data.UserId = cartDTO.UserId;
            data.ProductId = cartDTO.ProductId;
            data.Image = cartDTO.Image;
            data.Quantity = cartDTO.Quantity;
            data.SelectedSize = cartDTO.SelectedSize;
            data.CreatedAt = DateTime.Now;

            _conteks.Carts.Add(data);
            _conteks.SaveChanges();
            return true;

        }
    }
}
