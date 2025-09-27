using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TradeManagement.Models;
using TradeManagement.Services;

namespace TradeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Trader")]
    public class PortfolioController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public PortfolioController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Trader,User")]
        public async Task<IActionResult> GetAllPortfolios()
        {
            var portfolios = await _tradeService.GetAllPortfoliosAsync();
            return Ok(portfolios);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Trader,User")]
        public async Task<IActionResult> GetPortfolioById(string id)
        {
            var portfolio = await _tradeService.GetPortfolioByIdAsync(id);
            if (portfolio == null) return NotFound();
            return Ok(portfolio);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePortfolio([FromBody] Portfolio portfolio)
        {
            var created = await _tradeService.CreatePortfolioAsync(portfolio);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePortfolio(string id, [FromBody] Portfolio portfolio)
        {
            var success = await _tradeService.UpdatePortfolioAsync(id, portfolio);
            if (!success) return NotFound();
            return Ok(portfolio);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortfolio(string id)
        {
            var success = await _tradeService.DeletePortfolioAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
