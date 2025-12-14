using backend.Models.Phong;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Phong.Response
{
    public class PhongResponse
    {
        public string maPhong { get; set; }

        public string maTang { get; set; }

        public string tenPhong { get; set; }

        public string? maLoaiPhong { get; set; }

        public int? sucChua { get; set; }

        public TinhTrang? tinhTrang { get; set; }

        public string? ghiChu { get; set; }
    }
}
