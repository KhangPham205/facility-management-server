using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Tang
    {
        [Key]
        public string maTang {  get; set; }

        [Required]
        public string maToa { get; set; }

        [Required]
        public string tenTang { get; set; }

        public string? ghiChu { get; set; }

        //ForeignKey

        [ForeignKey("maToa")]
        public Toa toa { get; set; }
    }
}
