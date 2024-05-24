using SampleAPI.Entities;

namespace SampleAPI.Repositories
{
    public interface IOrderRepository
    {
        // TODO: Create repository methods.

        // Suggestions for repo methods:
        // public GetRecentOrders();
        // public AddNewOrder();
        List<Order> GetAllActiveOrders();
        List<Order> GetRecentOrders();
        Order AddOrder(Order objOrder);
        Order GetOrderById(int id);
    }
}
