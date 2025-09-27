using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TradeManagement.Models;
using TradeManagement.Services;

namespace TradeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Trader")]
    public class OrdersController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public OrdersController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Trader,User")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _tradeService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Trader,User")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            var order = await _tradeService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            var created = await _tradeService.CreateOrderAsync(order);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(string id, [FromBody] Order order)
        {
            var success = await _tradeService.UpdateOrderAsync(id, order);
            if (!success) return NotFound();
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var success = await _tradeService.DeleteOrderAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
