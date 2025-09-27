using Microsoft.AspNetCore.Mvc;
using TradeManagement.Models;
using TradeManagement.Services;

namespace TradeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TradesController : ControllerBase
    {
        private readonly TradeService _tradeService;

        public TradesController(TradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Trade>>> GetAll()
        {
            var trades = await _tradeService.GetAllTradesAsync();
            return Ok(trades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trade>> GetById(string id)
        {
            var trade = await _tradeService.GetTradeByIdAsync(id);
            if (trade == null) return NotFound($"Trade with id {id} not found.");
            return Ok(trade);
        }

        [HttpPost]
        public async Task<ActionResult<Trade>> Create(Trade trade)
        {
            var created = await _tradeService.CreateTradeAsync(trade);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Trade trade)
        {
            var updated = await _tradeService.UpdateTradeAsync(id, trade);
            if (!updated) return NotFound($"Trade with id {id} not found.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var deleted = await _tradeService.DeleteTradeAsync(id);
            if (!deleted) return NotFound($"Trade with id {id} not found.");
            return NoContent();
        }
    }
}
