namespace TradeManagement.Models
{
    public class LoginRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<string>? Roles { get; set; } // optional, for registration
    }
}
