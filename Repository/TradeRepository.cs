using TradeManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeManagement.Repository
{
    public class TradeRepository : ITradeRepository
    {
        private readonly IMongoCollection<Trade> _trades;

        public TradeRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _trades = database.GetCollection<Trade>(settings.Value.TradesCollectionName);
        }

        public async Task<List<Trade>> GetAllTradesAsync() =>
            await _trades.Find(trade => true).ToListAsync();

        public async Task<Trade?> GetTradeByIdAsync(string id) =>
            await _trades.Find(trade => trade.Id == id).FirstOrDefaultAsync();

        public async Task<Trade> CreateTradeAsync(Trade trade)
        {
            await _trades.InsertOneAsync(trade);
            return trade;
        }

        public async Task<bool> UpdateTradeAsync(string id, Trade trade)
        {
            var result = await _trades.ReplaceOneAsync(t => t.Id == id, trade);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteTradeAsync(string id)
        {
            var result = await _trades.DeleteOneAsync(t => t.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
