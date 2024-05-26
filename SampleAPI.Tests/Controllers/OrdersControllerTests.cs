using Moq;
using SampleAPI.Entities;
using SampleAPI.Manager;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using SampleAPI.Tests.Repositories;
using Assert = Xunit.Assert;

namespace SampleAPI.Tests.Controllers
{
    public class OrdersControllerTests
    {
        // TODO: Write controller unit tests
        private readonly Mock<IOrderManager> _serviceMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        public OrdersControllerTests()
        {
            _serviceMock = new Mock<IOrderManager>(MockBehavior.Strict);
            _orderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
        }
        [Fact]
        public async Task GetRecentOrder()
        {
            _serviceMock.Setup(x => x.GetAllActiveOrders()).ReturnsAsync(new List<Order>()); // Set up the behavior for GetOrder

            var orderProcessor = new OrderProcessor(_serviceMock.Object);

            // Act
             List<Order> result =  orderProcessor.GetAllActiveOrders().Result;

            // Assert
            Assert.Equal(new List<Order>(), result);
        }
        [Fact]
        public async Task CreateOrder()
        {
            _serviceMock.Setup(x => x.AddOrder(It.IsAny<CreateOrderRequest>())).ReturnsAsync(new Order()); // Set up the behavior for CreateOrder

            var orderProcessor = new OrderProcessor(_serviceMock.Object);

            // Act
            var result = orderProcessor.AddOrder(new CreateOrderRequest());

            // Assert
            Assert.Equal(0, result.Result.Id);
        }

        [Fact]
        public async Task DeleteOrder()
        {
            _serviceMock.Setup(x => x.DeleteOrder(1,1)).ReturnsAsync(true); // Set up the behavior for delete

            var orderProcessor = new OrderProcessor(_serviceMock.Object);

            // Act
            bool result = orderProcessor.DeleteOrder(1,1).Result;

            // Assert
            Assert.Equal(true, result);
        }

        [Fact]
        public void GetRecentOrdersByDays()
        {
            // Arrange

            var orderService = new OrderManager(_orderRepositoryMock.Object);
            int days = 5;
            // Set up mocks and test data

            _serviceMock.Setup(x => x.GetRecentOrdersByDays(days)).ReturnsAsync(new List<Order>());

            var orderProcessor = new OrderProcessor(_serviceMock.Object);
            // Act
            var result = orderProcessor.GetRecentOrdersByDays(5).Result;

            // Assert
            Assert.Equal(new List<Order>(), result);
        }
    }
}
