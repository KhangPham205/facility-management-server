namespace backend.DTOs.Audit.Response
{
    public class AuditDetailResponse
    {
        public string DetailId { get; set; }
        public string EquipmentName { get; set; }
        public int BookQuantity { get; set; }
        public int ActualQuantity { get; set; }
        public int Difference { get; set; }
    }
}
