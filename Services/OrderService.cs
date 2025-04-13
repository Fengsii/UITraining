using UITraining.Interfaces;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using UITraining.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using static UITraining.Models.GeneralStatus;
using Microsoft.EntityFrameworkCore;

namespace UITraining.Services
{
    public class OrderService : IOrder
    {
        private readonly ApplicationContext _conteks;

        public OrderService(ApplicationContext conteks)
        {
            _conteks = conteks;
        }
        public List<OrderDTO> GetlistOrder()
        {
            var data = _conteks.Orders.Include(y => y.User)
                .Select(x => new OrderDTO
                {
                    Id = x.Id,
                    OrderCode = x.OrderCode,
                    UserName = x.User.Username,
                    Image = x.Image,
                    Status = x.Status,

                }).ToList();

            return data;
        }


        public Order GetOrderById(int id)
        {
            var data = _conteks.Orders.FirstOrDefault();
            if (data == null)
            {
                return new Order();
            }

            return data;
        }

        public bool EditOrder(OrderDTO orderDTO)
        {
            var data = _conteks.Orders.FirstOrDefault(x => x.Id == orderDTO.Id);
            if (data == null)
            {
                return false;
            }

            data.OrderCode = orderDTO.OrderCode;
            data.UserId = orderDTO.UserId;
            data.Image = orderDTO.Image;
            data.Status = orderDTO.Status;

            _conteks.Orders.Update(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool DeleteOrder(int id)
        {
            var data = _conteks.Orders.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return false;
            }

            //data.ProductStatus = GeneralStatusData.delete;
            _conteks.Orders.Remove(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool AddOrder(OrderDTO orderDTO)
        {
            var data = new Order();

            data.OrderCode = orderDTO.OrderCode;
            data.UserId = orderDTO.UserId;
            data.Image = orderDTO.Image;
            data.Status = orderDTO.Status;
            data.OrderDate = DateTime.Now;

            _conteks.Orders.Add(data);
            _conteks.SaveChanges();
            return true;

        }

        public List<SelectListItem> Orders()
        {
            var datas = _conteks.Orders
                .Select(x => new SelectListItem
                {
                    Text = x.OrderCode,
                    Value = x.Id.ToString()
                }).ToList();


            return datas;
        }

    }
}
