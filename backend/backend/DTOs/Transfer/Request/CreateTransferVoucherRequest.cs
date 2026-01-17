namespace backend.DTOs.Transfer.Request
{
    public class CreateTransferVoucherRequest
    {
        public string RequestId { get; set; }
        public string CreatedBy { get; set; }
        public List<TransferRequestDetailDto> Details { get; set; }
    }
}
