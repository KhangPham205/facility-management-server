using backend.Models.Area;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IBuildingRepository
    {
        Task<Building?> GetByIdAsync(string id);

        Task<PageVO<Building>> GetPagedAsync(EntityFilter<Building> filter, EntitySort<Building> sort, int pageNumber, int pageSize);
        Task AddAsync(Building building);
        Task UpdateAsync(Building building);
        Task DeleteAsync(Building building);

        Task<bool> ExistsAsync(string id);
    }
}