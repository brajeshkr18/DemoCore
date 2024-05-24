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
        public async Task<List<Order>> GetAllActiveOrders()
        {
            return await _orderRepository.GetAllActiveOrders();
        }
        public async Task<List<Order>> GetRecentOrders()
        {
            return await _orderRepository.GetRecentOrders();
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await _orderRepository.GetOrderById(id);
        }

        public async Task<Order> AddOrder(Order objOrder)
        {
            return await _orderRepository.AddOrder(objOrder);
        }

        public async Task<bool> RemoveOrder(int id, int lastUpdatedBy)
        {
            return await _orderRepository.RemoveOrder(id, lastUpdatedBy);
        }
    }
}
