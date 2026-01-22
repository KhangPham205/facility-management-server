namespace backend.DTOs.Statistics
{
    public class SemesterCostDTO
    {
        public string SemesterName { get; set; }
        public decimal TotalCost { get; set; }
        public double Percentage { get; set; } // Tỷ lệ phần trăm so với tổng chi phí
    }
}
