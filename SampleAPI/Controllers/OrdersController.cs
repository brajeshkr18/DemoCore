using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Entities;
using SampleAPI.Manager;
using SampleAPI.Mappings;
using SampleAPI.Requests;
using System.Security.Claims;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        private readonly IMapper _autoMapperProfiles;

        // Add more dependencies as needed.

        public OrdersController(IOrderManager orderManager, IMapper autoMapperProfiles)
        {
            _orderManager = orderManager;
            this._autoMapperProfiles = autoMapperProfiles;
        }

        [HttpGet("GetAllActiveOrders")] // TODO: Change route, if needed.
        [ProducesResponseType(StatusCodes.Status200OK)] // TODO: Add all response types
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            return await _orderManager.GetAllActiveOrders();
        }

        [HttpGet("GetRecentOrders")] // TODO: Change route, if needed.
        [ProducesResponseType(StatusCodes.Status200OK)] // TODO: Add all response types
        public async Task<ActionResult<List<Order>>> GetRecentOrders()
        {
            return await _orderManager.GetRecentOrders();
        }

        /// TODO: Add an endpoint to allow users to create an order using <see cref="CreateOrderRequest"/>.

        #region Create Remove Order

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest orderRequest)
        {
            //TODO Need to implement Authentication //Convert.ToInt32(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            orderRequest.CreatedBy = 1;
            var orderDomainModel = _autoMapperProfiles.Map<Order>(orderRequest);
            await _orderManager.AddOrder(orderDomainModel);
            return Ok(_autoMapperProfiles.Map<CreateOrderRequest>(orderDomainModel));
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> RemoveOrder([FromRoute] int id)
        {
            var result = await _orderManager.RemoveOrder(id, 1);
            return Ok(result);
        }


        #endregion
    }
}
