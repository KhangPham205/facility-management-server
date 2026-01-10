using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Transfer
{
    public class CreateTransferDTO
    {
        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public string EquipmentId { get; set; }

        [Required]
        public string SourceLocation { get; set; }

        [Required]
        public string DestinationLocation { get; set; }

        public string? Reason { get; set; }
    }
}
