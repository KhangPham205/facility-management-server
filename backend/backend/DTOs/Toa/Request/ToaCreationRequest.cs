using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Toa.Request
{
    public class ToaCreationRequest
    {
        // Mã Toa (maToa) sẽ được sinh tự động hoặc trong Service, không cần gửi lên.

        [Required(ErrorMessage = "Tên Toà là bắt buộc.")]
        [StringLength(maximumLength: 100, ErrorMessage = "Tên Toà không được vượt quá 100 ký tự.")]
        public string tenToa { get; set; }

        [Required(ErrorMessage = "Số lượng tầng là bắt buộc.")]
        [Range(minimum: 1, maximum: 100, ErrorMessage = "Số lượng tầng phải từ 1 đến 100.")]
        public int soLuongTang { get; set; }

        [StringLength(maximumLength: 500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự.")]
        public string? ghiChu { get; set; }
    }
}
