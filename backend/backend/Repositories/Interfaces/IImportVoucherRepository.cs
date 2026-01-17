using backend.Enums;
using backend.Models;
using backend.Models.Import;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IImportVoucherRepository
    {
        Task<ImportVoucher?> GetByIdAsync(string id);

        Task<PageVO<ImportVoucher>> GetPagedAsync(EntityFilter<ImportVoucher> filter, EntitySort<ImportVoucher> sort, int pageNumber, int pageSize);
        Task AddAsync(ImportVoucher importVoucher);
        Task UpdateAsync(ImportVoucher importVoucher);
        Task DeleteAsync(ImportVoucher importVoucher);

        Task<bool> ExistsAsync(string id);
    }
}