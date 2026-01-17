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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public string ImportId { get; set; }
        public string RequestId { get; set; }
        public string? InvoiceId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public User Creator { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        public Invoice Invoice { get; set; }

        [ForeignKey(nameof(RequestId))]
        public ImportRequest Request { get; set; }

        public ICollection<ImportVoucherDetail> Details { get; set; }
    }
}