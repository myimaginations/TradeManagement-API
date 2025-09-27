using TradeManagement.Models;

namespace TradeManagement.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(string id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<List<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(string id, User updatedUser);
        Task DeleteUserAsync(string id);
        Task<bool> AdminExistsAsync();
    }
}
