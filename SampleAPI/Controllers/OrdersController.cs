using Microsoft.AspNetCore.Mvc;
using SampleAPI.Entities;
using SampleAPI.Helper;
using SampleAPI.Manager;
using SampleAPI.Requests;
using System.Runtime.CompilerServices;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        private readonly ILogger<OrdersController> _logger;
        // Add more dependencies as needed.
        static string GetActualAsyncMethodName([CallerMemberName] string name = null) => name;
        public OrdersController(IOrderManager orderManager, ILogger<OrdersController> logger)
        {
            _orderManager = orderManager;
            this._logger = logger;
        }
        #region Get Recent Orders
        [HttpGet("GetRecentOrders")] // TODO: Change route, if needed.
        public async Task<IActionResult> GetRecentOrders()
        {
            try
            {
                var recentOrders = await _orderManager.GetRecentOrders();
                if (recentOrders == null || recentOrders.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent, new ResponseJson<List<Order>>().GetNoData(null));
                }
                else
                    return StatusCode(StatusCodes.Status200OK, new ResponseJson<List<Order>>().GetOK(recentOrders));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Start_Time=={DateTime.UtcNow}, Controller={this.GetType().Name},"
                + $"MethodName={GetActualAsyncMethodName()}, IsException=Yes ReturnObject=StackTrace {ex.StackTrace}, Message {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseJson<List<Order>>().GetInternalServerError(ex));
            }

        }
        #endregion
        /// TODO: Add an endpoint to allow users to create an order using <see cref="CreateOrderRequest"/>.

        #region Create Order
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest orderRequest)
        {
            try
            {
                //TODO Need to implement Authentication 

                //if we implement Authentication then we can consider created by as
                //Convert.ToInt32(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

                var result = await _orderManager.AddOrder(orderRequest);
                return StatusCode(StatusCodes.Status200OK, new ResponseJson<Order>().GetOK(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Start_Time=={DateTime.UtcNow}, Controller={this.GetType().Name},"
                + $"MethodName={GetActualAsyncMethodName()}, IsException=Yes ReturnObject=StackTrace {ex.StackTrace}, Message {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseJson<List<Order>>().GetInternalServerError(ex));
            }
        }
        #endregion

        #region Delete Order
        [HttpPost("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromQuery] int id)
        {
            try 
            {
                //if we implement Authentication then we can consider deleted by as
                //Convert.ToInt32(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

                var result = await _orderManager.DeleteOrder(id, 1);//set hardcode user as 1
                if (result)
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseJson<bool>().GetOK(result));
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent,false);
                }
                
            }
            
            catch (Exception ex)
            {
                _logger.LogError($"Start_Time=={DateTime.UtcNow}, Controller={this.GetType().Name},"
                + $"MethodName={GetActualAsyncMethodName()}, IsException=Yes ReturnObject=StackTrace {ex.StackTrace}, Message {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseJson<List<Order>>().GetInternalServerError(ex));
            }
        }
        #endregion

        #region Recent Order By Days
        [HttpGet("GetRecentOrderByDays")]
        public async Task<IActionResult> GetRecentOrdersByDays(int days)
        {
            try
            {
                var orders = await _orderManager.GetRecentOrdersByDays(days);
                if (orders == null || orders.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent, new ResponseJson<List<Order>>().GetNoData(null));
                }
                else
                    return StatusCode(StatusCodes.Status200OK, new ResponseJson<List<Order>>().GetOK(orders));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Start_Time=={DateTime.UtcNow}, Controller={this.GetType().Name},"
                + $"MethodName={GetActualAsyncMethodName()}, IsException=Yes ReturnObject=StackTrace {ex.StackTrace}, Message {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseJson<List<Order>>().GetInternalServerError(ex));
            }
            
        }
        #endregion
    }
}
