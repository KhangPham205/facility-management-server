using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.ImportVoucher.Request
{
    public class ImportVoucherCreationRequest
    {
        [Required]
        public string SupplierId { get; set; }

        public string InvoiceId { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public string Purpose { get; set; }
    }
}