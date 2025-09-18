using Microsoft.AspNetCore.Mvc;
using TradeManagement.Models;
using TradeManagement.Services;

namespace TradeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly TradeService _tradeService;

        public PortfolioController(TradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<Portfolio>> GetUserPortfolio(string userId)
        {
            var portfolio = await _tradeService.GetPortfolioAsync(userId);
            if (portfolio == null)
            {
                return NotFound();
            }
            return portfolio;
        }
    }
}