using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Import.Request
{
    public class UpdateImportRequestStatusRequest
    {
        [Required]
        public VoucherStatus Status { get; set; } // Approved / Rejected

        [Required]
        public string ApprovedBy { get; set; }
    }
}
