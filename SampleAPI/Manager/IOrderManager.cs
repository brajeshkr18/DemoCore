using SampleAPI.Entities;
using SampleAPI.Requests;

namespace SampleAPI.Manager
{
    public interface IOrderManager
    {
        // TODO: Create repository methods.

        // Suggestions for repo methods:
        // public GetRecentOrders();
        // public AddNewOrder();
        Task<List<Order>> GetAllActiveOrders();
        Task<List<Order>> GetRecentOrders();
        Task<Order> AddOrder(CreateOrderRequest objRequestOrder);
        Task<Order> GetOrderById(int id);
        Task<bool> DeleteOrder(int id, int lastUpdatedBy);
        Task<List<Order>> GetRecentOrdersByDays(int numberOfDays);

    }
}
