using Microsoft.EntityFrameworkCore;
using SampleAPI.Entities;
using SampleAPI.Repositories;

namespace SampleAPI.Manager
{
    public class OrderManager : IOrderManager
    {
        protected IOrderRepository _orderRepository;
        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> GetAllActiveOrders()
        {
            return _orderRepository.GetAllActiveOrders();
        }
        public List<Order> GetRecentOrders()
        {
            return _orderRepository.GetRecentOrders();
        }
        public Order GetOrderById(int id)
        {
            return _orderRepository.GetOrderById(id);
        }

        public Order AddOrder(Order objOrder)
        {
            return _orderRepository.AddOrder(objOrder);
        }
    }
}
