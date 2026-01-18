using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.DTOs.Audit.Response
{
    public class PeriodicAuditResponse
    {
        public string PeriodId { get; set; }
        public string AuditName { get; set; } // VD: Kiểm kê Quý 1/2024
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string ResponsiblePerson { get; set; }

    }
}