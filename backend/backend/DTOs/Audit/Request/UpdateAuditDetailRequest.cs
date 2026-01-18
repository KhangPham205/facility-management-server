using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Audit.Request
{
    public class UpdateAuditDetailRequest
    {
        //public int BookQuantity { get; set; }
        //public int ActualQuantity { get; set; }

        //public int Difference { get; set; }

        public EquipmentStatus? Condition { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}
