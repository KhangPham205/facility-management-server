using backend.Enums;

namespace backend.DTOs.Transfer.Response
{
    public class TransferRequestResponse
    {
        public string RequestId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string SourceLocationName { get; set; }
        public string DestinationLocationName { get; set; }
        public string Note { get; set; }
        public VoucherStatus Status { get; set; }
        public List<TransferRequestDetailResponse> Details { get; set; }
    }
}
