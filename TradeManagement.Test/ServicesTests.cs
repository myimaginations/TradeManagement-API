using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TradeManagement.Models;
using TradeManagement.Repositories;
using TradeManagement.Services;
using Xunit;

namespace TradeManagement.Test
{
    public class ServicesTests
    {
        private readonly Mock<IUserRepository> mockUserRepo;
        private readonly Mock<ITradeRepository> mockTradeRepo;
        private readonly Mock<IOrderRepository> mockOrderRepo;
        private readonly Mock<IInstrumentRepository> mockInstrumentRepo;
        private readonly Mock<IPortfolioRepository> mockPortfolioRepo;
        private readonly Mock<ILogger<TradeService>> mockLogger;
        private readonly TradeService service;

        public ServicesTests()
        {
            mockUserRepo = new Mock<IUserRepository>();
            mockTradeRepo = new Mock<ITradeRepository>();
            mockOrderRepo = new Mock<IOrderRepository>();
            mockInstrumentRepo = new Mock<IInstrumentRepository>();
            mockPortfolioRepo = new Mock<IPortfolioRepository>();
            mockLogger = new Mock<ILogger<TradeService>>();

            service = new TradeService(
                mockUserRepo.Object,
                mockTradeRepo.Object,
                mockOrderRepo.Object,
                mockInstrumentRepo.Object,
                mockPortfolioRepo.Object,
                mockLogger.Object
            );
        }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnUsers()
        {
            mockUserRepo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<User>
                {
                    new User { Id = "1", Name = "Alice" }
                });

            var users = await service.GetAllUsersAsync();
            Assert.Single(users);
            Assert.Equal("Alice", users.First().Name);
        }

        [Fact]
        public async Task GetTradeByIdAsync_ShouldReturnTrade()
        {
            // Cast to Trade? to match the nullable return type
            mockTradeRepo.Setup(r => r.GetByIdAsync("1"))
                .ReturnsAsync((Trade?)new Trade { Id = "1", Quantity = 10 });

            var trade = await service.GetTradeByIdAsync("1");
            Assert.NotNull(trade);
            Assert.Equal(10, trade?.Quantity);
        }

        [Fact]
        public async Task GetTradeByIdAsync_InvalidId_ShouldReturnNull()
        {
            mockTradeRepo.Setup(r => r.GetByIdAsync("invalid"))
                .ReturnsAsync((Trade?)null);

            var trade = await service.GetTradeByIdAsync("invalid");
            Assert.Null(trade);
        }
    }
}
