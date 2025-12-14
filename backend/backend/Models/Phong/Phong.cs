using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Phong
{
    public class Phong
    {
        [Key]
        public string maPhong { get; set; }

        [Required]
        public string maTang { get; set; }

        [Required]
        public string tenPhong { get; set; }

        public string? maLoaiPhong {  get; set; }

        public int? sucChua { get; set; }
        
        public TinhTrang? tinhTrang { get; set; }
        
        public string? ghiChu { get; set; }

        //================================================

        [ForeignKey("maTang")]
        public Tang tang {  get; set; }
    }
}
