using backend.Enums;
using backend.Models.Finance;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Maintenance
{
    [Table("MaintenanceVouchers")]
    public class MaintenanceVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string VoucherId { get; set; }

        public string RequestId { get; set; }
        [ForeignKey("RequestId")]
        public MaintenanceRequest Request { get; set; }

        public string? InvoiceId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public MaintenanceStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [ForeignKey("InvoiceId")]
        public Invoice? Invoice { get; set; }
        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }

        public ICollection<MaintenanceVoucherDetail> Details { get; set; }
    }
}
