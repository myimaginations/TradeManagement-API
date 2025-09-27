using TradeManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeManagement.Repository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(string id);
        Task<Order> CreateOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(string id, Order order);
        Task<bool> DeleteOrderAsync(string id);
    }
}
