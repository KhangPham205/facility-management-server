namespace backend.DTOs.Transfer.Request
{
    public class CreateTransferVoucherRequest
    {
        public string RequestId { get; set; }
        public List<TransferRequestDetailDto> Details { get; set; }
    }
}
