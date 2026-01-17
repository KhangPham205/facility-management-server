using backend.Enums;
using backend.Models.Finance;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Repair
{
    [Table("RepairVouchers")]
    public class RepairVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RepairId { get; set; }

        public string RequestId { get; set; }
        [ForeignKey("RequestId")]
        public RepairRequest Request { get; set; }

        public string? InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public Invoice? Invoice { get; set; }

        public string? ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public ExternalUnit? Provider { get; set; }

        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public MaintenanceStatus Status { get; set; }

        public ICollection<RepairVoucherDetail> Details { get; set; }
    }
}
