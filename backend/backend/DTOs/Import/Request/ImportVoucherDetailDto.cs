using backend.Enums;

namespace backend.DTOs.Import.Request
{
    public class ImportVoucherDetailDto
    {
        public string EquipmentName { get; set; }
        public string? Note { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
