using backend.Enums;
using backend.Models.BaseInvoidAndVoucher;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Repair
{
    public class RepairRequest
    {
        [Key]
        //[Column("report_id")]
        public string ReportId { get; set; } = null!;

        //[Column("created_by")]
        public string CreatedBy { get; set; } = null!;

        //[Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //[Column("voucher_detail_id")]
        public string VoucherDetailId { get; set; } = null!;

        //[Column("reason")]
        public string? Reason { get; set; }

        //[Column("status")]
        public ReqMaintenanceStatus Status { get; set; }

        //[Column("status_updated_by")]
        public string? StatusUpdatedBy { get; set; }

        //[Column("status_updated_at")]
        public DateTime? StatusUpdatedAt { get; set; }

        // --- Navigation properties ---
        [ForeignKey("CreatedBy")]
        public virtual User Creator { get; set; } = null!;

        [ForeignKey("VoucherDetailId")]
        public virtual ICollection<VoucherDetail> VoucherDetail { get; set; } = new List<VoucherDetail>();

        [ForeignKey("StatusUpdatedBy")]
        public virtual User? StatusUpdater { get; set; }

        public virtual ICollection<RepairVoucher> RepairVouchers { get; set; } = new List<RepairVoucher>();
    }
}
