using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TradeManagement.Models;
using TradeManagement.Services;

namespace TradeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Trader")]
    public class TradesController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradesController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Trader,User")]
        public async Task<IActionResult> GetAllTrades()
        {
            var trades = await _tradeService.GetAllTradesAsync();
            return Ok(trades);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Trader,User")]
        public async Task<IActionResult> GetTradeById(string id)
        {
            var trade = await _tradeService.GetTradeByIdAsync(id);
            if (trade == null) return NotFound();
            return Ok(trade);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrade([FromBody] Trade trade)
        {
            var created = await _tradeService.CreateTradeAsync(trade);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrade(string id, [FromBody] Trade trade)
        {
            var success = await _tradeService.UpdateTradeAsync(id, trade);
            if (!success) return NotFound();
            return Ok(trade);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrade(string id)
        {
            var success = await _tradeService.DeleteTradeAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
