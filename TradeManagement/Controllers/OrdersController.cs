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

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(Order newOrder)
        {
            var user = await _tradeService.GetUserByIdAsync(newOrder.UserId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var instruments = await _tradeService.GetInstrumentsAsync();
            var instrument = instruments.FirstOrDefault(i => i.Symbol == newOrder.Symbol);
            if (instrument == null)
            {
                return BadRequest("Instrument not found.");
            }

            // Simple Buy/Sell logic
            if (newOrder.Type.ToUpper() == "BUY")
            {
                var cost = newOrder.Quantity * instrument.Price;
                if (user.Balance < cost)
                {
                    return BadRequest("Insufficient funds.");
                }
                user.Balance -= cost;

                // Update portfolio
                var portfolio = await _tradeService.GetPortfolioAsync(user.Id);
                if (portfolio == null)
                {
                    portfolio = new Portfolio { UserId = user.Id, Holdings = new List<Holding>() };
                    await _tradeService.CreatePortfolioAsync(portfolio);
                }

                var holding = portfolio.Holdings.FirstOrDefault(h => h.Symbol == newOrder.Symbol);
                if (holding != null)
                {
                    holding.Quantity += newOrder.Quantity;
                }
                else
                {
                    portfolio.Holdings.Add(new Holding { Symbol = newOrder.Symbol, Quantity = newOrder.Quantity });
                }
                await _tradeService.UpdatePortfolioAsync(user.Id, portfolio);
            }
            else if (newOrder.Type.ToUpper() == "SELL")
            {
                var portfolio = await _tradeService.GetPortfolioAsync(user.Id);
                var holding = portfolio?.Holdings.FirstOrDefault(h => h.Symbol == newOrder.Symbol);
                if (holding == null || holding.Quantity < newOrder.Quantity)
                {
                    return BadRequest("Not enough shares to sell.");
                }
                user.Balance += newOrder.Quantity * instrument.Price;
                holding.Quantity -= newOrder.Quantity;
                await _tradeService.UpdatePortfolioAsync(user.Id, portfolio);
            }

            await _tradeService.UpdateUserAsync(user.Id, user);
            await _tradeService.CreateOrderAsync(newOrder);

            return CreatedAtAction(nameof(GetUserOrders), new { userId = newOrder.UserId }, newOrder);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Order>>> GetUserOrders(string userId)
        {
            var orders = await _tradeService.GetUserOrdersAsync(userId);
            return orders;
        }
    }
}