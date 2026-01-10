using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Maintenance
{
    public class MaintenanceRequest
    {
        [Key]
        //[Column("maintenance_request_id")]
        public string MaintenanceRequestId { get; set; } = null!;

        //[Column("requester_id")]
        public string CreatedBy { get; set; } = null!;

        //[Column("requested_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //[Column("equipment_id")]
        public string VoucherDetailId { get; set; } = null!;

        //[Column("reason")]
        public string? Reason { get; set; }

        //[Column("status")]
        public ReqMaintenanceStatus Status { get; set; } = ReqMaintenanceStatus.Pending;

        //[Column("status_updated_by")]
        public string? StatusUpdatedBy { get; set; }

        //[Column("status_updated_at")]
        public DateTime? StatusUpdatedAt { get; set; }

        // --- Navigation properties ---
        [ForeignKey("CreatedBy")]
        public virtual User Requester { get; set; } = null!;

        [ForeignKey("VoucherDetailId")]
        public virtual ICollection<VoucherDetail> VoucherDetail { get; set; } = new List<VoucherDetail>();

        [ForeignKey("StatusUpdatedBy")]
        public virtual User? StatusUpdater { get; set; }

        public virtual ICollection<MaintenanceVoucher> MaintenanceVouchers { get; set; } = new List<MaintenanceVoucher>();
    }
}
