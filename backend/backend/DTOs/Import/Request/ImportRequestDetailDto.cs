namespace backend.DTOs.Import.Request
{
    public class ImportRequestDetailDto
    {
        public string EquipmentName { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
    }
}
