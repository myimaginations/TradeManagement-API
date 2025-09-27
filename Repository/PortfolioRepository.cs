using TradeManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeManagement.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly IMongoCollection<Portfolio> _portfolios;

        public PortfolioRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _portfolios = database.GetCollection<Portfolio>(settings.Value.PortfoliosCollectionName);
        }

        public async Task<List<Portfolio>> GetAllPortfoliosAsync() =>
            await _portfolios.Find(p => true).ToListAsync();

        public async Task<Portfolio?> GetPortfolioByIdAsync(string id) =>
            await _portfolios.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio)
        {
            await _portfolios.InsertOneAsync(portfolio);
            return portfolio;
        }

        public async Task<bool> UpdatePortfolioAsync(string id, Portfolio portfolio)
        {
            var result = await _portfolios.ReplaceOneAsync(p => p.Id == id, portfolio);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeletePortfolioAsync(string id)
        {
            var result = await _portfolios.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
