using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.FundSource.Request
{
    public class CreateFundSourceRequest
    {
        [Required(ErrorMessage = "Tên nguồn kinh phí là bắt buộc.")]
        [StringLength(200)]
        public string SourceName { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn hoặc bằng 0.")]
        public decimal Amount { get; set; }

        public string? Description { get; set; }
    }
}
