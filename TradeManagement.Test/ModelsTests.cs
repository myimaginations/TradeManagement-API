using TradeManagement.Models;
using Xunit;

namespace TradeManagement.Test
{
    public class ModelsTests
    {
        [Fact]
        public void CreateUser_ShouldSetProperties()
        {
            var user = new User { Id = "1", Name = "Alice", Email = "alice@example.com" };
            Assert.Equal("1", user.Id);
            Assert.Equal("Alice", user.Name);
            Assert.Equal("alice@example.com", user.Email);
        }

        [Fact]
        public void CreateOrder_ShouldSetProperties()
        {
            var order = new Order
            {
                Id = "1",
                InstrumentId = "inst1",
                Quantity = 10,
                Price = 100.5m,
                OrderType = "Buy",
                UserId = "user1"
            };

            Assert.Equal("1", order.Id);
            Assert.Equal("inst1", order.InstrumentId);
            Assert.Equal(10, order.Quantity);
            Assert.Equal(100.5m, order.Price);
            Assert.Equal("Buy", order.OrderType);
            Assert.Equal("user1", order.UserId);
        }

        [Fact]
        public void CreateTrade_ShouldSetProperties()
        {
            var trade = new Trade
            {
                Id = "1",
                OrderId = "order1",
                Quantity = 5,
                Price = 50m
            };

            Assert.Equal("1", trade.Id);
            Assert.Equal("order1", trade.OrderId);
            Assert.Equal(5, trade.Quantity);
            Assert.Equal(50m, trade.Price);
        }
    }
}
