using backend.Models.EquipmentInfo;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface ICriteriaRepository
    {
        Task<Criteria?> GetByIdAsync(string id);

        Task<PageVO<Criteria>> GetPagedAsync(EntityFilter<Criteria> filter, EntitySort<Criteria> sort, int pageNumber, int pageSize);
        Task AddAsync(Criteria criteria);
        Task AddRangeAsync(IEnumerable<Criteria> criterias);
        Task UpdateAsync(Criteria criteria);
        Task DeleteAsync(Criteria criteria);

        Task<bool> ExistsAsync(string id);
    }
}