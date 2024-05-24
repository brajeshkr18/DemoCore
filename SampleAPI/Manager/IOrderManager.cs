using SampleAPI.Entities;

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
        Task<Order> AddOrder(Order objOrder);
        Task<Order> GetOrderById(int id);
        Task<bool> RemoveOrder(int id, int lastUpdatedBy);

    }
}
