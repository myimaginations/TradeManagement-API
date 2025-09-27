using TradeManagement.Models;
using MongoDB.Driver;

namespace TradeManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public async Task<User?> GetUserByIdAsync(string id) =>
            await _users.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task<User?> GetUserByUsernameAsync(string username) =>
            await _users.Find(u => u.Username == username).FirstOrDefaultAsync();

        public async Task<List<User>> GetAllUsersAsync() =>
            await _users.Find(_ => true).ToListAsync();

        public async Task AddUserAsync(User user)
        {
            if (user.Roles.Contains("Admin") && await AdminExistsAsync())
                throw new InvalidOperationException("An Admin already exists.");

            await _users.InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(string id, User updatedUser) =>
            await _users.ReplaceOneAsync(u => u.Id == id, updatedUser);

        public async Task DeleteUserAsync(string id) =>
            await _users.DeleteOneAsync(u => u.Id == id);

        public async Task<bool> AdminExistsAsync() =>
            await _users.Find(u => u.Roles.Contains("Admin")).AnyAsync();
    }
}
