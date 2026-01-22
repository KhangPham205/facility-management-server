namespace backend.DTOs.Statistics
{
    public class DeviceStatisticDTO
    {
        public int TotalDevices { get; set; }
        public List<MetricDTO> ByCategory { get; set; } = new();
        public List<MetricDTO> ByStatus { get; set; } = new();
        // Thống kê theo tên (thường là Top thiết bị nhiều nhất)
        public List<MetricDTO> ByName { get; set; } = new();
    }
}
