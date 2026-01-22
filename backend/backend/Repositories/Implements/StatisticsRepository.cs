using backend.Data;
using backend.DTOs.Statistics;
using backend.Models.EquipmentInfo;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class StatisticsRepository : IStatisticsRepository
{
    private readonly DataApplicationDbContext _context;

    public StatisticsRepository(DataApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DeviceStatisticDTO> GetDeviceStatsAsync()
    {
        var query = _context.Equipments.AsNoTracking();

        var total = await query.CountAsync();

        var byCategory = await query
            .GroupBy(e => e.Category.EquipmentCategoryName)
            .Select(g => new MetricDTO { Label = g.Key, Count = g.Count() })
            .ToListAsync();

        var statusRaw = await query
            .GroupBy(e => e.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToListAsync();

        var byStatus = statusRaw.Select(x => new MetricDTO
        {
            Label = x.Status.ToString(),
            Count = x.Count
        }).ToList();

        var byName = await query
            .GroupBy(e => e.EquipmentName)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new MetricDTO { Label = g.Key, Count = g.Count() })
            .ToListAsync();

        return new DeviceStatisticDTO
        {
            TotalDevices = total,
            ByCategory = byCategory,
            ByStatus = byStatus,
            ByName = byName
        };
    }

    public async Task<List<BorrowTrendDTO>> GetBorrowTrendAsync(DateTime from, DateTime to)
    {
        return await _context.BorrowVouchers
            .AsNoTracking()
            .Where(b => b.CreatedAt >= from && b.CreatedAt <= to)
            .GroupBy(b => b.CreatedAt.Date)
            .Select(g => new BorrowTrendDTO
            {
                Date = g.Key,
                Count = g.Count()
            })
            .OrderBy(x => x.Date)
            .ToListAsync();
    }

    // Lấy list thiết bị trong năm để Service tự tính toán chia học kỳ
    public async Task<List<Equipment>> GetEquipmentForCostAsync(int year)
    {
        return await _context.Equipments
            .AsNoTracking()
            .Where(e => e.CreatedAt.Year == year || e.CreatedAt.Year == year - 1)
            .Select(e => new Equipment { CreatedAt = e.CreatedAt, UnitPrice = e.UnitPrice })
            .ToListAsync();
    }
}