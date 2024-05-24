using Microsoft.AspNetCore.Mvc;
using SampleAPI.Entities;
using SampleAPI.Manager;
using SampleAPI.Requests;
using System.Security.Claims;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        // Add more dependencies as needed.

        public OrdersController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpGet("GetAllActiveOrders")] // TODO: Change route, if needed.
        [ProducesResponseType(StatusCodes.Status200OK)] // TODO: Add all response types
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            return _orderManager.GetAllActiveOrders();
        }

        [HttpGet("GetRecentOrders")] // TODO: Change route, if needed.
        [ProducesResponseType(StatusCodes.Status200OK)] // TODO: Add all response types
        public async Task<ActionResult<List<Order>>> GetRecentOrders()
        {
            return _orderManager.GetRecentOrders();
        }

        /// TODO: Add an endpoint to allow users to create an order using <see cref="CreateOrderRequest"/>.

        #region Create Order

        [HttpPost]
        [Route("CreateOrder")]
        public IActionResult Create([FromBody] Order order)
        {
            
            order.CreatedBy = 1; //TODO Need to implement Authentication // Convert.ToInt32(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            order.CreatedDate = DateTime.UtcNow;
            try
            {
                _orderManager.AddOrder(order);
                return StatusCode(StatusCodes.Status200OK, order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion
    }
}
