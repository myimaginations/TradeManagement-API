using TradeManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeManagement.Repository
{
    public interface IPortfolioRepository
    {
        Task<List<Portfolio>> GetAllPortfoliosAsync();
        Task<Portfolio?> GetPortfolioByIdAsync(string id);
        Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio);
        Task<bool> UpdatePortfolioAsync(string id, Portfolio portfolio);
        Task<bool> DeletePortfolioAsync(string id);
    }
}
