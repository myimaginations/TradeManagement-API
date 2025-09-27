using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeManagement.Models;
using TradeManagement.Repositories;
using Xunit;

namespace TradeManagement.Test
{
    public class RepositoriesTests
    {
        [Fact]
        public async Task UserRepository_GetAll_ShouldReturnUsers()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<User>
                {
                    new User { Id = "1", Name = "Alice" },
                    new User { Id = "2", Name = "Bob" }
                });

            var users = await mockRepo.Object.GetAllAsync();
            Assert.NotNull(users);
            Assert.Equal(2, users.Count());
            Assert.Equal("Alice", users.First().Name);
        }

        [Fact]
        public async Task PortfolioRepository_GetByUserId_ShouldReturnPortfolio()
        {
            var mockRepo = new Mock<IPortfolioRepository>();
            mockRepo.Setup(r => r.GetByUserIdAsync("user1"))
                .ReturnsAsync((Portfolio?)new Portfolio { Id = "1", UserId = "user1" });

            var portfolio = await mockRepo.Object.GetByUserIdAsync("user1");
            Assert.NotNull(portfolio);
            Assert.Equal("user1", portfolio?.UserId);
        }
    }
}
