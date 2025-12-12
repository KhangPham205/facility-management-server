using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.PhieuChuyen
{
    public class ChiTietPhieuChuyen
    {
        [Key]
        public int STT { get; set; }

        [Required]
        public string MaPC { get; set; }

        public string MaTB { get; set; }
        public int SoLuong { get; set; }

        [ForeignKey("MaPC")]
        public PhieuChuyen PhieuChuyen { get; set; }
    }
}
