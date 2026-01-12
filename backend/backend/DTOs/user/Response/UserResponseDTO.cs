using backend.Enums;

namespace backend.DTOs.user.Response
{
    public class UserResponseDTO
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
