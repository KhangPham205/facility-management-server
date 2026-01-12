using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.FundSource.Request
{
    public class UpdateFundSourceRequest
    {
        [Required(ErrorMessage = "Tên nguồn kinh phí là bắt buộc.")]
        [StringLength(200)]
        public string SourceName { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        public string? Description { get; set; }
    }
}
