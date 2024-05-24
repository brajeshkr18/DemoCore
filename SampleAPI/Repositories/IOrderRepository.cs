using SampleAPI.Entities;

namespace SampleAPI.Repositories
{
    public interface IOrderRepository
    {
        // TODO: Create repository methods.

        // Suggestions for repo methods:
        // public GetRecentOrders();
        // public AddNewOrder();
        Task<List<Order>> GetAllActiveOrders();
        Task<List<Order>> GetRecentOrders();
        Task<Order> AddOrder(Order objOrder);
        Task<Order> GetOrderById(int id);
    }
}
