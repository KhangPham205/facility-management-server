using backend.Enums;
using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Maintenance
{
    [Table("MaintenanceRequests")]
    [EntityFilter(Prefix = "")]
    public class MaintenanceRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RequestId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Note { get; set; }
        public VoucherStatus Status { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }

        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }
        [ForeignKey("ApprovedBy")]
        public User? Approver { get; set; }

        public ICollection<MaintenanceRequestDetail> Details { get; set; }
    }
}
