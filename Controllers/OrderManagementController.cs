using Microsoft.AspNetCore.Mvc;
using Mysqlx.Crud;
using OrderManagementSystem.Model;
using OrderManagementSystem.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrderManagementController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderManagementController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderInfo>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderInfo>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> PostOrder(OrderInfo order)
        {
            await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderID }, order);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutOrder(int id, OrderInfo order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }
            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
        [HttpPost("delete-multiple")]
        public async Task<IActionResult> DeleteMultipleOrders([FromBody] List<int> orderIDs)
        {
            if (orderIDs == null || orderIDs.Count == 0)
            {
                return BadRequest("No order IDs provided.");
            }

            try
            {
                await _orderService.DeleteMultipleOrdersAsync(orderIDs);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
