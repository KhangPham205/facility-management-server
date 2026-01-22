using backend.Data;
using backend.Enums;
using backend.Models.Area;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class AreaRepository : IAreaRepository
    {
        private readonly DataApplicationDbContext _context;

        public AreaRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<String?> GetNameByIdAsync(LocationType type, string id)
        {
            switch (type)
            {
                case LocationType.Building:
                    return await _context.Buildings
                        .Where(r => r.BuildingId == id)
                        .Select(r => r.BuildingName)
                        .FirstOrDefaultAsync();
                case LocationType.Floor:
                    return await _context.Floors
                        .Where(r => r.FloorId == id)
                        .Select(r => r.FloorName)
                        .FirstOrDefaultAsync();
                case LocationType.Room:
                    return await _context.Rooms
                        .Where(r => r.RoomId == id)
                        .Select(r => r.RoomName)
                        .FirstOrDefaultAsync();
                default:
                    return null;
            }
        }

        public async Task<Dictionary<string, string>> GetNamesByIdsAsync(LocationType type, List<string> ids)
        {
            if (ids == null || !ids.Any()) return new Dictionary<string, string>();

            switch (type)
            {
                case LocationType.Building:
                    return await _context.Buildings
                        .Where(b => ids.Contains(b.BuildingId))
                        .ToDictionaryAsync(b => b.BuildingId, b => b.BuildingName);

                case LocationType.Floor:
                    return await _context.Floors
                        .Where(f => ids.Contains(f.FloorId))
                        .ToDictionaryAsync(f => f.FloorId, f => f.FloorName);

                case LocationType.Room:
                    return await _context.Rooms
                        .Where(r => ids.Contains(r.RoomId))
                        .ToDictionaryAsync(r => r.RoomId, r => r.RoomName);

                default:
                    return new Dictionary<string, string>();
            }
        }
    }
}