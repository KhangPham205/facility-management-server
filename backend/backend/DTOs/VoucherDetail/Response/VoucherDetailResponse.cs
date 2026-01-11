namespace backend.DTOs.VoucherDetail.Response
{
    public class VoucherDetailResponse
    {
        public string VoucherDetailId { get; set; } 
        public string EquipmentId { get; set; } 
        public string CategoryId { get; set; } 
        public string EquipmentName { get; set; } 
        public int Quantity { get; set; } 
        public decimal UnitPrice { get; set; } 
    }
}