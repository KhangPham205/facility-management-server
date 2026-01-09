using backend.Models;

namespace backend.DTOs.Auth
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserDetailDTO user { get; set; }
    }
}
