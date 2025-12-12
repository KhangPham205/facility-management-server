using System.ComponentModel.DataAnnotations;

namespace backend.Models.PhieuChuyen
{
    public class PhieuChuyen
    {
        [Key]
        public string MaPC { get; set; }

        public string NoiGui { get; set; }
        public string NoiNhan { get; set; }
        public string NguoiLap { get; set; }
        public DateTime NgayLap { get; set; }

        public string NguyenNhan { get; set; }
        public TrangThaiPhieuChuyen TrangThai { get; set; }

        public string? NguoiDoiTT { get; set; }
        public DateTime? NgayDoiTT { get; set; }
        public string? TrangThaiDoi { get; set; }

        public string? NguoiDuyet { get; set; }
        public DateTime? NgayDuyet { get; set; }

        public List<ChiTietPhieuChuyen> ChiTiet { get; set; }

    }
}
