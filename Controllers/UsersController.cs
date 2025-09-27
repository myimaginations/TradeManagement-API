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

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var users = await _tradeService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            var user = await _tradeService.GetUserByIdAsync(id);
            if (user == null) return NotFound($"User with id {id} not found.");
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            var created = await _tradeService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, User user)
        {
            var updated = await _tradeService.UpdateUserAsync(id, user);
            if (!updated) return NotFound($"User with id {id} not found.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var deleted = await _tradeService.RemoveUserAsync(id);
            if (!deleted) return NotFound($"User with id {id} not found.");
            return NoContent();
        }
    }
}
