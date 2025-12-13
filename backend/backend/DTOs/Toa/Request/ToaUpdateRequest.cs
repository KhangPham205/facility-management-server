using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Toa.Request
{
    public class ToaUpdateRequest
    {
        [Required(ErrorMessage = "Tên Toà là bắt buộc.")]
        [StringLength(maximumLength: 100, ErrorMessage = "Tên Toà không được vượt quá 100 ký tự.")]
        public string tenToa { get; set; } // Tên Toà là bắt buộc phải gửi lên

        [Required(ErrorMessage = "Số lượng tầng là bắt buộc.")]
        [Range(1, 100, ErrorMessage = "Số lượng tầng phải từ 1 đến 100.")]
        public int soLuongTang { get; set; }

        [StringLength(maximumLength: 500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string? ghiChu { get; set; }
    }
}
