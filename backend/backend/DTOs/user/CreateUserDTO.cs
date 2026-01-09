using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.user
{
    public class CreateUserDTO
    {
        [Required]
        public string Fullname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        //[MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { get; set; }

        public UserRole Role { get; set; }
    }
}
