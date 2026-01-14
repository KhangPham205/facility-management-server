namespace backend.DTOs.Transfer.Response
{
    public class TransferVoucherResponse
    {
        public string TransferId { get; set; }
        public string RequestId { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string SourceLocationId { get; set; }
        public string DestinationRoomId { get; set; }

        public List<TransferVoucherDetailResponse> Details { get; set; }
    }
}
