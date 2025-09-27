using TradeManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeManagement.Services
{
    public interface ITradeService
    {
        // Instrument
        Task<List<Instrument>> GetAllInstrumentsAsync();
        Task<Instrument?> GetInstrumentByIdAsync(string id);
        Task<Instrument> CreateInstrumentAsync(Instrument instrument);
        Task<bool> UpdateInstrumentAsync(string id, Instrument instrument);
        Task<bool> DeleteInstrumentAsync(string id);

        // Order
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(string id);
        Task<Order> CreateOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(string id, Order order);
        Task<bool> DeleteOrderAsync(string id);

        // Trade
        Task<List<Trade>> GetAllTradesAsync();
        Task<Trade?> GetTradeByIdAsync(string id);
        Task<Trade> CreateTradeAsync(Trade trade);
        Task<bool> UpdateTradeAsync(string id, Trade trade);
        Task<bool> DeleteTradeAsync(string id);

        // Portfolio
        Task<List<Portfolio>> GetAllPortfoliosAsync();
        Task<Portfolio?> GetPortfolioByIdAsync(string id);
        Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio);
        Task<bool> UpdatePortfolioAsync(string id, Portfolio portfolio);
        Task<bool> DeletePortfolioAsync(string id);
    }
}
