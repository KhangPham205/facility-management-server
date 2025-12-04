using System.ComponentModel.DataAnnotations;

namespace backend.Models.TaiKhoan
{
    public class TaiKhoan
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string TenTK { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string MatKhau { get; set; }

        public DateTime NgayTao { get; set; }

        public VaiTro VaiTro { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
