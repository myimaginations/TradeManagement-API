using TradeManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeManagement.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _orders = database.GetCollection<Order>(settings.Value.OrdersCollectionName);
        }

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
    }
}
