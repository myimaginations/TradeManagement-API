using Moq;
using TradeManagement.Controllers;
using TradeManagement.Models;
using TradeManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeManagement.Test
{
    public class ControllersTests
    {
        private readonly Mock<ITradeService> _mockService = new();

        [Fact]
        public async Task UsersController_GetAllUsers_ShouldReturnOk()
        {
            _mockService.Setup(s => s.GetAllUsersAsync())
                        .ReturnsAsync(new List<User> { new User { Id = "u1", Name = "Alice" } });

            var controller = new UsersController(_mockService.Object);
            var result = await controller.GetUsers();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsType<List<User>>(okResult.Value);
            Assert.Single(users);
        }

        [Fact]
        public async Task TradesController_GetAllTrades_ShouldReturnEmptyList()
        {
            _mockService.Setup(s => s.GetAllTradesAsync())
                        .ReturnsAsync(new List<Trade>());

            var controller = new TradesController(_mockService.Object);
            var result = await controller.GetAllTrades();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var trades = Assert.IsType<List<Trade>>(okResult.Value);
            Assert.Empty(trades);
        }

        [Fact]
        public async Task UsersController_GetUserById_ShouldReturnNotFound()
        {
            _mockService.Setup(s => s.GetUserByIdAsync("invalid"))
                        .ReturnsAsync((User?)null); // ✅ explicitly nullable

            var controller = new UsersController(_mockService.Object);
            var result = await controller.GetUser("invalid");

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
