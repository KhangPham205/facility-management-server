using backend.Enums;
using backend.Models.Import;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IImportRequestDetailRepository
    {
        Task<ImportRequestDetail?> GetByIdAsync(string id);

        Task<PageVO<ImportRequestDetail>> GetPagedAsync(EntityFilter<ImportRequestDetail> filter, EntitySort<ImportRequestDetail> sort, int pageNumber, int pageSize);
        Task AddAsync(ImportRequestDetail importRequestDetail);
        //Task UpdateAsync(ImportRequestDetail importRequestDetail);
        //Task<bool> UpdateStatusAsync(string importRequestDetailId, string approverId, VoucherStatus newStatus);
        Task DeleteAsync(ImportRequestDetail importRequestDetail);

        Task<bool> ExistsAsync(string id);
    }
}