using Microsoft.EntityFrameworkCore;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;

namespace SampleAPI.Manager
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository _orderRepository;
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

        public async Task<Order> AddOrder(CreateOrderRequest objRequestOrder)
        {
            var objOrder = new Order()
            {
                Name = objRequestOrder.Name,
                Description = objRequestOrder.Description,
                CreatedBy = objRequestOrder.CreatedBy,
            };
            return await _orderRepository.AddOrder(objOrder);
        }

        public async Task<bool> DeleteOrder(int id, int lastUpdatedBy)
        {
            return await _orderRepository.DeleteOrder(id, lastUpdatedBy);
        }
        public async Task<List<Order>> GetRecentOrdersByDays(int numberOfDays)
        {
            return await _orderRepository.GetRecentOrdersByDays(numberOfDays);
        }
    }
}
