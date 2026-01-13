using backend.Models.Transfer;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface ITransferRequestRepository
    {
        Task<PageVO<TransferRequest>> GetPagedAsync(EntityFilter<TransferRequest> filter, EntitySort<TransferRequest> sort, int page, int size);
        Task<TransferRequest?> GetByIdAsync(string id);
        Task AddAsync(TransferRequest request);
        Task UpdateAsync(TransferRequest request);
        Task DeleteAsync(TransferRequest request);
    }
}
