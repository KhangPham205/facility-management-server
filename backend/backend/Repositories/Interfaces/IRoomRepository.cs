using backend.Models.Area;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room?> GetByIdAsync(string id);

        Task<PageVO<Room>> GetPagedAsync(EntityFilter<Room> filter, EntitySort<Room> sort, int pageNumber, int pageSize);
        Task AddAsync(Room room);
        Task UpdateAsync(Room room);
        Task DeleteAsync(Room room);

        Task<bool> ExistsAsync(string id);
    }
}