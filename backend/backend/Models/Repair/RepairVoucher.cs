using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Repair
{
    public class RepairVoucher
    {
        [Key]
        public string RepairId { get; set; }

        public string ReportId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string InvoiceId { get; set; }

        public string VoucherDetailId { get; set; }

        public MaintenanceStatus Status { get; set; }

        public string StatusUpdatedBy { get; set; }

        public DateTime? StatusUpdatedAt { get; set; }

        // --- Navigation properties ---
        [ForeignKey("ReportId")]
        public virtual RepairRequest DamageReport { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User Creator { get; set; }
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
        [ForeignKey("StatusUpdatedBy")]
        public virtual User StatusUpdater { get; set; }
    }
}
