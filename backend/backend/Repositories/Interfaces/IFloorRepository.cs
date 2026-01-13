using backend.Models.Area;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IFloorRepository
    {
        Task<Floor?> GetByIdAsync(string id);

        Task<PageVO<Floor>> GetPagedAsync(EntityFilter<Floor> filter, EntitySort<Floor> sort, int pageNumber, int pageSize);
        Task AddAsync(Floor floor);
        Task UpdateAsync(Floor floor);
        Task DeleteAsync(Floor floor);

        Task<bool> ExistsAsync(string id);
    }
}