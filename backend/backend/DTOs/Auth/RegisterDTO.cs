using backend.Models.TaiKhoan;

namespace backend.DTOs.Auth
{
    public class RegisterDTO
    {
        public string TenTK { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public VaiTro VaiTro { get; set; }
    }
}
