using Microsoft.AspNetCore.Mvc;
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

        // GET: api/Portfolio/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<object>> GetPortfolio(string userId)
        {
            // Get user info (includes balance & portfolio)
            var user = await _tradeService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            // Return combined portfolio info
            var response = new
            {
                UserId = user.Id,
                Balance = user.Balance,
                Holdings = user.Portfolio // Dictionary<string, int> Instrument -> Quantity
            };

            return Ok(response);
        }
    }
}
