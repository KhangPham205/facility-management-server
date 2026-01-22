using backend.Models.Finance;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<PageVO<Invoice>> GetPagedAsync(EntityFilter<Invoice> filter, EntitySort<Invoice> sort, int page, int size);
        Task<Invoice?> GetByIdAsync(string id);
        Task AddAsync(Invoice invoice);
        //Task UpdateAsync(Invoice invoice);
        Task DeleteAsync(Invoice invoice);
    }
}
