using backend.Models.Finance;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IExternalUnitRepository
    {
        Task<PageVO<ExternalUnit>> GetPagedAsync(EntityFilter<ExternalUnit> filter, EntitySort<ExternalUnit> sort, int page, int size);
        Task<ExternalUnit?> GetByIdAsync(string id);
        Task AddAsync(ExternalUnit unit);
        Task UpdateAsync(ExternalUnit unit);
        Task DeleteAsync(ExternalUnit unit);
    }
}
