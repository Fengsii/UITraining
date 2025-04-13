using Microsoft.AspNetCore.Mvc.Rendering;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Interfaces
{
    public interface IOrder
    {
        public List<OrderDTO> GetlistOrder();
        public Order GetOrderById(int id);
        public bool EditOrder(OrderDTO orderDTO);
        public bool DeleteOrder(int id);
        public bool AddOrder(OrderDTO orderDTO);
        public List<SelectListItem> Orders();

    }
}
