using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SampleAPI.Entities;
using SampleAPI.Manager;
using SampleAPI.Repositories;

namespace SampleAPI.Tests.Controllers
{
    [TestClass()]
    public class OrdersControllerTests
    {
        // TODO: Write controller unit tests
        private IOrderManager _sut;
        private readonly IOrderRepository _orderRepository = Substitute.For<IOrderRepository>();

        [TestInitialize]
        public void Initialize() => _sut = new OrderManager(_orderRepository);
        [TestMethod]
        [Fact]
        public void GetOrder()
        {
            //Arrange
            List<int> ids = new() { 1, 2, 3, 4, 5 };
            var mockOrder = CreateOrderList(ids);
            _orderRepository.GetAllActiveOrders().Returns(mockOrder);

            //Act
            var orders = _sut.GetAllActiveOrders();

            //Assert
            orders.Count.Equals(mockOrder?.Count);
        }
        private static List<Order> CreateOrderList(List<int> ids)
        {
            List<Order> orders = new();
            foreach (int id in ids)
            {
                orders.Add(new Order() { Id = id, Name = "ABC" + id, LastUpdateDate = DateTime.UtcNow });
            };
            return orders;
        }
    }
}
