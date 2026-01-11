using backend.Enums;
using backend.Models.BaseInvoidAndVoucher;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Maintenance
{
    public class MaintenanceVoucher
    {
        [Key]
        //[Column("maintenance_id")]
        public string MaintenanceId { get; set; } = null!;

        //[Column("maintenance_request_id")]
        public string MaintenanceRequestId { get; set; } = null!;

        //[Column("invoice_id")]
        public string? InvoiceId { get; set; }

        //[Column("voucher_detail_id")]
        public string VoucherDetailId { get; set; } = null!;

        //[Column("created_by")]
        public string CreatedBy { get; set; } = null!;

        //[Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //[Column("status")]
        public MaintenanceStatus Status { get; set; }

        //[Column("status_updated_by")]
        public string? StatusUpdatedBy { get; set; }

        //[Column("status_updated_at")]
        public DateTime? StatusUpdatedAt { get; set; }

        // --- Navigation properties ---
        [ForeignKey("MaintenanceRequestId")]
        public virtual MaintenanceRequest MaintenanceRequest { get; set; } = null!;

        [ForeignKey("InvoiceId")]
        public virtual Invoice? Invoice { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User Creator { get; set; } = null!;

        [ForeignKey("StatusUpdatedBy")]
        public virtual User? StatusUpdater { get; set; }
    }
}
