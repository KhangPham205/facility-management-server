using backend.Models.Phong;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Phong.Request
{
    public class PhongUpdateRequest
    {
        [Required]
        public string maTang { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Tên phòng không được vượt quá 100 ký tự.")]
        public string tenPhong { get; set; }

        public string? maLoaiPhong { get; set; }

        public int? sucChua { get; set; }

        public TinhTrang? tinhTrang { get; set; }

        public string? ghiChu { get; set; }
    }
}
