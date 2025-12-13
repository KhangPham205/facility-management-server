using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Tang.Request
{
    public class TangUpdateRequest
    {
        [Required]
        public string maToa { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Tên tầng không được vượt quá 100 ký tự")]
        public string tenTang { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string? ghiChu { get; set; }
    }
}
