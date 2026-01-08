using System.ComponentModel.DataAnnotations;

namespace backend.Models.User
{
    public class User
    {
        [Key]
        public string UserId { get; set; } // Diagram: UserId (string)

        [Required]
        public string Fullname { get; set; } // Diagram: Fullname

        [Required]
        [EmailAddress]
        public string Email { get; set; } // Diagram: Email

        [Required]
        public string Password { get; set; } // Diagram: Password

        public DateTime CreatedAt { get; set; } // Diagram: CreatedAt

        public UserRole Role { get; set; } // Diagram: Role

        // Fields for Auth Logic (not in ERD but required for code to work)
        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
