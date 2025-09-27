using TradeManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeManagement.Repository
{
    public interface ITradeRepository
    {
        Task<List<Trade>> GetAllTradesAsync();
        Task<Trade?> GetTradeByIdAsync(string id);
        Task<Trade> CreateTradeAsync(Trade trade);
        Task<bool> UpdateTradeAsync(string id, Trade trade);
        Task<bool> DeleteTradeAsync(string id);
    }
}
