using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TradeManagement.Models;

namespace TradeManagement.Services
{
    public class TradeService
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IMongoCollection<Instrument> _instrumentsCollection;
        private readonly IMongoCollection<Order> _ordersCollection;
        private readonly IMongoCollection<Portfolio> _portfoliosCollection;

        public TradeService(IOptions<MongoDbSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>("Users");
            _instrumentsCollection = mongoDatabase.GetCollection<Instrument>("Instruments");
            _ordersCollection = mongoDatabase.GetCollection<Order>("Orders");
            _portfoliosCollection = mongoDatabase.GetCollection<Portfolio>("Portfolios");
        }

        // User methods
        public async Task CreateUserAsync(User user) => await _usersCollection.InsertOneAsync(user);
        public async Task<User?> GetUserByIdAsync(string id) => await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        public async Task<List<User>> GetAllUsersAsync() => await _usersCollection.Find(_ => true).ToListAsync();
        public async Task UpdateUserAsync(string id, User updatedUser) => await _usersCollection.ReplaceOneAsync(u => u.Id == id, updatedUser);
        public async Task RemoveUserAsync(string id) => await _usersCollection.DeleteOneAsync(u => u.Id == id);

        // Instrument methods
        public async Task<List<Instrument>> GetInstrumentsAsync() => await _instrumentsCollection.Find(_ => true).ToListAsync();

        // Order methods
        public async Task CreateOrderAsync(Order order) => await _ordersCollection.InsertOneAsync(order);
        public async Task<List<Order>> GetUserOrdersAsync(string userId) => await _ordersCollection.Find(o => o.UserId == userId).ToListAsync();
        public async Task RemoveOrderAsync(string id) => await _ordersCollection.DeleteOneAsync(o => o.Id == id);

        // Portfolio methods
        public async Task CreatePortfolioAsync(Portfolio portfolio) => await _portfoliosCollection.InsertOneAsync(portfolio);
        public async Task<Portfolio?> GetPortfolioAsync(string userId) => await _portfoliosCollection.Find(p => p.UserId == userId).FirstOrDefaultAsync();
        public async Task UpdatePortfolioAsync(string userId, Portfolio updatedPortfolio) => await _portfoliosCollection.ReplaceOneAsync(p => p.UserId == userId, updatedPortfolio);
    }
}