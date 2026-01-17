using backend.Enums;
using backend.Models.Import;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IImportRequestRepository
    {
        Task<ImportRequest?> GetByIdAsync(string id);

        Task<PageVO<ImportRequest>> GetPagedAsync(EntityFilter<ImportRequest> filter, EntitySort<ImportRequest> sort, int pageNumber, int pageSize);
        Task AddAsync(ImportRequest importRequest);
        Task UpdateAsync(ImportRequest importRequest);
        Task<bool> UpdateStatusAsync(string importRequestId, VoucherStatus newStatus);
        Task DeleteAsync(ImportRequest importRequest);

        Task<bool> ExistsAsync(string id);
    }
}