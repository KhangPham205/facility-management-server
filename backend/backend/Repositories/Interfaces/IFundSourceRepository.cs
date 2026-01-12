using backend.Models.Finance;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IFundSourceRepository
    {
        Task<FundSource?> GetByIdAsync(string id);

        Task<PageVO<FundSource>> GetPagedAsync(EntityFilter<FundSource> filter, EntitySort<FundSource> sort, int pageNumber, int pageSize);
        Task AddAsync(FundSource fundSource);
        Task UpdateAsync(FundSource fundSource);
        Task DeleteAsync(FundSource fundSource);

        Task<bool> ExistsAsync(string id);
    }
}
