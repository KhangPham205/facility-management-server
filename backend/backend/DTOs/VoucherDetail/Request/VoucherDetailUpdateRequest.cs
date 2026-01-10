using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.VoucherDetail.Request
{
    public class VoucherDetailUpdateRequest
    {
        [Required]
        public string EquipmentName { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }
    }
}