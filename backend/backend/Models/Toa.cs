using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Toa
    {
        [Key]
        public string maToa { get; set; }

        [Required]
        public string tenToa { get; set; }

        [Required]
        public int soLuongTang { get; set; }

        public string ghiChu { get; set; }
    }
}