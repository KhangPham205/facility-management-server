using backend.Enums;

namespace backend.DTOs.user.Request
{
    public class UpdateUserDTO
    {
        public string Fullname { get; set; }
        public UserRole Role { get; set; }
    }
}
