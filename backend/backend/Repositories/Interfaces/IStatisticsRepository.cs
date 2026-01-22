using backend.DTOs.Statistics;
using backend.Models.EquipmentInfo;

namespace backend.Repositories.Interfaces
{
    public interface IStatisticsRepository
    {
        Task<DeviceStatisticDTO> GetDeviceStatsAsync();
        Task<List<BorrowTrendDTO>> GetBorrowTrendAsync(DateTime from, DateTime to);
        Task<List<Equipment>> GetEquipmentForCostAsync(int year);
    }
}
