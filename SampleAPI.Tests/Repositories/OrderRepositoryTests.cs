using FluentAssertions;
using FluentAssertions.Extensions;
using SampleAPI.Entities;
using SampleAPI.Manager;
using SampleAPI.Repositories;
using SampleAPI.Requests;

namespace SampleAPI.Tests.Repositories
{
    //public class OrderRepositoryTests
    //{
    //    // TODO: Write repository unit tests
    //}
    public class OrderProcessor: IOrderManager
    {
        private readonly IOrderManager _orderManager;

        public OrderProcessor(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public Task<Order> AddOrder(CreateOrderRequest order)
        {
            // Process the order and then add it using the order manager
            return _orderManager.AddOrder(order);
        }
        public Task<List<Order>> GetAllActiveOrders()
        {
            // Process the order and then add it using the order manager
            return _orderManager.GetAllActiveOrders();
        }
        public Task<bool> DeleteOrder(int id,int userId)
        {
            // Process the order and then add it using the order manager
            return _orderManager.DeleteOrder(id,userId);
        }
        public Task<List<Order>> GetRecentOrders()
        {
            // Process the order and then add it using the order manager
            return _orderManager.GetRecentOrders();
        }

        public Task<Order> GetOrderById(int id)
        {
            return _orderManager.GetOrderById(id);
        }
        public Task<List<Order>> GetRecentOrdersByDays(int days)
        {
            // Process the order and then add it using the order manager
            return _orderManager.GetRecentOrdersByDays(days);
        }
    }
}