using backend.Enums;

namespace backend.DTOs.Transfer.Request
{
    public class CreateTransferRequestRequest
    {
        public string CreatedBy { get; set; }

        public string SourceLocationId { get; set; }
        public LocationType SourceLocationType { get; set; }

        public string DestinationRoomId { get; set; }
        public LocationType DestinationLocationType { get; set; }

        public string? Reason { get; set; }
        public List<TransferRequestDetailDto> Details { get; set; } = new();
    }
}
