using backend.Models.Finance;
using backend.Models.Import;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("ImportVouchers")]
    public class ImportVoucher
    {
        [Key]
        public string ImportId { get; set; }
        public string RequestId { get; set; }
        public string SupplierId { get; set; }
        public string? InvoiceId { get; set; }
        public string FundingSourceId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("RequestId")]
        public ImportRequest Request { get; set; }
        [ForeignKey("SupplierId")]
        public ExternalUnit Supplier { get; set; }
        [ForeignKey("FundingSourceId")]
        public FundSource FundSource { get; set; }

        public ICollection<ImportVoucherDetail> Details { get; set; }
    }
}