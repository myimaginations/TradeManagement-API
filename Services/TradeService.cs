using TradeManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeManagement.Services
{
    public class TradeService : ITradeService
    {
        private readonly IMongoCollection<Instrument> _instruments;
        private readonly IMongoCollection<Order> _orders;
        private readonly IMongoCollection<Trade> _trades;
        private readonly IMongoCollection<Portfolio> _portfolios;

        public TradeService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var db = client.GetDatabase(settings.Value.DatabaseName);

            _instruments = db.GetCollection<Instrument>(settings.Value.InstrumentsCollectionName);
            _orders = db.GetCollection<Order>(settings.Value.OrdersCollectionName);
            _trades = db.GetCollection<Trade>(settings.Value.TradesCollectionName);
            _portfolios = db.GetCollection<Portfolio>(settings.Value.PortfoliosCollectionName);
        }

        #region Instruments
        public async Task<List<Instrument>> GetAllInstrumentsAsync() =>
            await _instruments.Find(i => true).ToListAsync();

        public async Task<Instrument?> GetInstrumentByIdAsync(string id) =>
            await _instruments.Find(i => i.Id == id).FirstOrDefaultAsync();

        public async Task<Instrument> CreateInstrumentAsync(Instrument instrument)
        {
            await _instruments.InsertOneAsync(instrument);
            return instrument;
        }

        public async Task<bool> UpdateInstrumentAsync(string id, Instrument instrument)
        {
            var result = await _instruments.ReplaceOneAsync(i => i.Id == id, instrument);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteInstrumentAsync(string id)
        {
            var result = await _instruments.DeleteOneAsync(i => i.Id == id);
            return result.DeletedCount > 0;
        }
        #endregion

        #region Orders
        public async Task<List<Order>> GetAllOrdersAsync() =>
            await _orders.Find(o => true).ToListAsync();

        public async Task<Order?> GetOrderByIdAsync(string id) =>
            await _orders.Find(o => o.Id == id).FirstOrDefaultAsync();

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _orders.InsertOneAsync(order);
            return order;
        }

        public async Task<bool> UpdateOrderAsync(string id, Order order)
        {
            var result = await _orders.ReplaceOneAsync(o => o.Id == id, order);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteOrderAsync(string id)
        {
            var result = await _orders.DeleteOneAsync(o => o.Id == id);
            return result.DeletedCount > 0;
        }
        #endregion

        #region Trades
        public async Task<List<Trade>> GetAllTradesAsync() =>
            await _trades.Find(t => true).ToListAsync();

        public async Task<Trade?> GetTradeByIdAsync(string id) =>
            await _trades.Find(t => t.Id == id).FirstOrDefaultAsync();

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
        #endregion

        #region Portfolios
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
        #endregion
    }
}
