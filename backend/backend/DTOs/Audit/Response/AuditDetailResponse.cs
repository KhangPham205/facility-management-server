using backend.Enums;

namespace backend.DTOs.Audit.Response
{
    public class AuditDetailResponse
    {
        public string AuditId { get; set; }

        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }


        //public int BookQuantity { get; set; }
        //public int ActualQuantity { get; set; }

        //public int Difference { get; set; }

        public EquipmentStatus? Condition { get; set; }

        public string? Note { get; set; }
    }
}
