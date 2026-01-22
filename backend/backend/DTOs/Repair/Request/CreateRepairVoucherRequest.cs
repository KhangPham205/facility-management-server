namespace backend.DTOs.Repair.Request
{
    public class CreateRepairVoucherRequest
    {
        public string RequestId { get; set; }
        public string InvoiceId { get; set; }
        public List<RepairRequestDetailDto> Details { get; set; }
    }
}
