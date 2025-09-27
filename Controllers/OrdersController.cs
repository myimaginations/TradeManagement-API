using Microsoft.AspNetCore.Mvc;
using TradeManagement.Models;
using TradeManagement.Services;

namespace TradeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly TradeService _tradeService;

        public OrdersController(TradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAll()
        {
            var orders = await _tradeService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(string id)
        {
            var order = await _tradeService.GetOrderByIdAsync(id);
            if (order == null) return NotFound($"Order with id {id} not found.");
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create(Order order)
        {
            var created = await _tradeService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Order order)
        {
            var updated = await _tradeService.UpdateOrderAsync(id, order);
            if (!updated) return NotFound($"Order with id {id} not found.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var deleted = await _tradeService.DeleteOrderAsync(id);
            if (!deleted) return NotFound($"Order with id {id} not found.");
            return NoContent();
        }
    }
}
