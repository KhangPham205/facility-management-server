using backend.DTOs.Invoice.Request;
using backend.DTOs.Invoice.Response;
using backend.Models.Finance;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IInvoiceService
    {
        public Task<PageVO<InvoiceResponse>> GetAll(EntityFilter<Invoice> filter, EntitySort<Invoice> sort, int page, int size);
        public Task<InvoiceResponse?> GetById(string id);
        public Task<InvoiceResponse> Create(CreateInvoiceRequest request);
        //public Task<InvoiceResponse> Update(string id, UpdateInvoiceRequest request);
        public Task Delete(string id);
    }
}
