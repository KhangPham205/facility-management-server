using backend.Enums;
using backend.Models.Import;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IImportVoucherDetailRepository
    {
        Task<ImportVoucherDetail?> GetByIdAsync(string id);

        Task<PageVO<ImportVoucherDetail>> GetPagedAsync(EntityFilter<ImportVoucherDetail> filter, EntitySort<ImportVoucherDetail> sort, int pageNumber, int pageSize);
        Task AddAsync(ImportVoucherDetail importVoucherDetail);
        //Task UpdateAsync(ImportVoucherDetail importVoucherDetail);
        //Task<bool> UpdateStatusAsync(string importVoucherDetailId, string approverId, VoucherStatus newStatus);
        Task DeleteAsync(ImportVoucherDetail importVoucherDetail);

        Task<bool> ExistsAsync(string id);
    }
}