using backend.Enums;
using backend.Models;

namespace backend.DTOs.Auth
{
    public class UserDetailDTO
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
