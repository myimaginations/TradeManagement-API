using Microsoft.AspNetCore.Mvc;
using TradeManagement.Models;
using TradeManagement.Services;

namespace TradeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly TradeService _tradeService;

        public UsersController(TradeService tradeService)
        {
            _tradeService = tradeService;
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await _tradeService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // GET api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _tradeService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // GET api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _tradeService.GetAllUsersAsync();
            return Ok(users);
        }

        // PUT api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User updatedUser)
        {
            var user = await _tradeService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            updatedUser.Id = user.Id;
            await _tradeService.UpdateUserAsync(id, updatedUser);
            return NoContent();
        }

        // DELETE api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _tradeService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _tradeService.RemoveUserAsync(id);
            return NoContent();
        }
    }
}