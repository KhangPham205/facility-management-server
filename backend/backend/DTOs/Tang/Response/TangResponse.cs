using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Tang.Response
{
    public class TangResponse
    {
        [Key]
        public string maTang { get; set; }

        [Required]
        public string maToa { get; set; }

        [Required]
        public string tenTang { get; set; }

        public string? ghiChu { get; set; }
    }
}
