using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Audit.Request
{
    public class CreatePeriodicAuditRequest
    {
        [Required]
        [StringLength(100)]
        public string PeriodicAuditName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ResponsiblePerson { get; set; }
    }
}
