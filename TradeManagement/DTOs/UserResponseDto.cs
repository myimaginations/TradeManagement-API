using System.Collections.Generic;

namespace TradeManagement.Models
{
    public class UserResponseDto
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public List<string> Roles { get; set; } = new List<string>();
        public string Token { get; set; } = null!;
    }
}
