using UITraining.Interfaces;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using UITraining.Models;
using Microsoft.EntityFrameworkCore;

namespace UITraining.Services
{
    public class OrderDetailService : IOrderDetail
    {
        private readonly ApplicationContext _conteks;

        public OrderDetailService (ApplicationContext conteks)
        {
            _conteks = conteks;
        }
        public List<OrderDetailDTO> GetlistOrderDetail()
        {
            var data = _conteks.OrderDetails.Include(y => y.Order).Include(w => w.Product)
                .Select(x => new OrderDetailDTO
                {
                    Id = x.Id,
                    OrderCode = x.Order.OrderCode,
                    ProductName = x.Product.Name,
                    Image = x.Image,
                    Quantity = x.Quantity,
                    SelectedSize = x.SelectedSize,
                    PriceAtPurchase = x.PriceAtPurchase,

                }).ToList();

            return data;
        }


        public OrderDetail GetOrderDetailById(int id)
        {
            var data = _conteks.OrderDetails.FirstOrDefault();
            if (data == null)
            {
                return new OrderDetail();
            }

            return data;
        }

        public bool EditOrderDetail(OrderDetailDTO orderDetailDTO)
        {
            var data = _conteks.OrderDetails.FirstOrDefault(x => x.Id == orderDetailDTO.Id);
            if (data == null)
            {
                return false;
            }

            data.OrderId = orderDetailDTO.OrderId;
            data.ProductId = orderDetailDTO.ProductId;
            data.Image = orderDetailDTO.Image;
            data.Quantity= orderDetailDTO.Quantity;
            data.SelectedSize = orderDetailDTO.SelectedSize;
            data.PriceAtPurchase = orderDetailDTO.PriceAtPurchase;

            _conteks.OrderDetails.Update(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool DeleteOrderDetail(int id)
        {
            var data = _conteks.OrderDetails.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return false;
            }

            //data.ProductStatus = GeneralStatusData.delete;
            _conteks.OrderDetails.Remove(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool AddOrderDetail(OrderDetailDTO orderDetailDTO)
        {
            var data = new OrderDetail();

            data.OrderId = orderDetailDTO.OrderId;
            data.ProductId = orderDetailDTO.ProductId;
            data.Image = orderDetailDTO.Image;
            data.Quantity = orderDetailDTO.Quantity;
            data.SelectedSize = orderDetailDTO.SelectedSize;
            data.PriceAtPurchase = orderDetailDTO.PriceAtPurchase;

            _conteks.OrderDetails.Add(data);
            _conteks.SaveChanges();
            return true;

        }
    }
}
