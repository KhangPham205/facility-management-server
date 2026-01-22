using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DTOs.Statistics;
using backend.Repositories.Interfaces;

namespace backend.Services.Implements
{
    public class StatisticsService
    {
        private readonly IStatisticsRepository _repo;

        public StatisticsService(IStatisticsRepository repo)
        {
            _repo = repo;
        }

        public async Task<DeviceStatisticDTO> GetDeviceStatistics()
        {
            return await _repo.GetDeviceStatsAsync();
        }

        public async Task<List<BorrowTrendDTO>> GetBorrowStatistics(DateTime? from, DateTime? to)
        {
            var startDate = from ?? DateTime.Now.AddDays(-30);
            var endDate = to ?? DateTime.Now;
            return await _repo.GetBorrowTrendAsync(startDate, endDate);
        }

        public async Task<List<SemesterCostDTO>> GetCostStatisticsBySemester(int year)
        {
            var equipments = await _repo.GetEquipmentForCostAsync(year);

            decimal totalCost = 0;
            var semesterMap = new Dictionary<string, decimal>();

            // Định nghĩa danh sách học kỳ cần báo cáo trong năm được chọn
            var periods = new List<(string Name, int MonthStart, int MonthEnd)>
            {
                ($"HK1 {year}-{year + 1}", 8, 12), // Tháng 8-12 năm nay
                ($"HK2 {year}-{year + 1}", 1, 5),  // Tháng 1-5 năm sau (hoặc năm nay tùy quy định trường)
                ($"HK Hè {year}", 6, 7)           // Tháng 6-7 năm nay
            };

            foreach (var eq in equipments)
            {
                string label = GetSemesterLabel(eq.CreatedAt);
                if (!string.IsNullOrEmpty(label))
                {
                    if (!semesterMap.ContainsKey(label)) semesterMap[label] = 0;
                    semesterMap[label] += eq.UnitPrice; // Cộng dồn giá trị nhập
                    totalCost += eq.UnitPrice;
                }
            }

            var result = semesterMap.Select(kvp => new SemesterCostDTO
            {
                SemesterName = kvp.Key,
                TotalCost = kvp.Value,
                Percentage = totalCost > 0 ? (double)Math.Round((kvp.Value / totalCost) * 100, 2) : 0
            }).ToList();

            return result;
        }

        private string GetSemesterLabel(DateTime date)
        {
            int month = date.Month;
            int year = date.Year;

            if (month >= 8 && month <= 12) return $"HK1 {year}-{year + 1}";
            if (month >= 1 && month <= 5) return $"HK2 {year - 1}-{year}";
            if (month >= 6 && month <= 7) return $"Hè {year}";

            return "Khác";
        }
    }
}
