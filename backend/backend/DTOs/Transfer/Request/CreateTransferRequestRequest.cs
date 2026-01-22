using backend.Enums;

namespace backend.DTOs.Transfer.Request
{
    public class CreateTransferRequestRequest
    {
        public string SourceLocationId { get; set; }
        public LocationType SourceLocationType { get; set; }

        public string DestinationLocationId { get; set; }
        public LocationType DestinationLocationType { get; set; }

        public string? Note { get; set; }
        public List<TransferRequestDetailDto> Details { get; set; } = new();
    }
}
