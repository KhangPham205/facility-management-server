using backend.Models.Area;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IRoomTypeRepository
    {
        Task<RoomType?> GetByIdAsync(string id);

        Task<PageVO<RoomType>> GetPagedAsync(EntityFilter<RoomType> filter, EntitySort<RoomType> sort, int pageNumber, int pageSize);
        Task AddAsync(RoomType roomType);
        Task UpdateAsync(RoomType roomType);
        Task DeleteAsync(RoomType roomType);

        Task<bool> ExistsAsync(string id);
    }
}