using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.VoucherDetail.Request
{
    public class VoucherDetailCreationRequest
    {
        [Required]
        public string EquipmentId { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        [StringLength(255)]
        public string EquipmentName { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }
    }
}