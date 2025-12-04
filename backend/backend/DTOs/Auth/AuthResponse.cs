using backend.Models.TaiKhoan;

namespace backend.DTOs.Auth
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public TaiKhoan User { get; set; }
    }
}
