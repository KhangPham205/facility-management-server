using AutoMapper;
using backend.DTOs.Invoice.Request;
using backend.DTOs.Invoice.Response;
using backend.Models.Finance;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _repository;
        private readonly IMapper _mapper;
        public InvoiceService(IInvoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PageVO<InvoiceResponse>> GetAll(EntityFilter<Invoice> filter, EntitySort<Invoice> sort, int page, int size)
        {
            var pagedResult = await _repository.GetPagedAsync(filter, sort, page, size);
            
            var dtoList = _mapper.Map<List<InvoiceResponse>>(pagedResult.Content);
            
            return new PageVO<InvoiceResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }
        public async Task<InvoiceResponse?> GetById(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<InvoiceResponse>(entity);
        }
        public async Task<InvoiceResponse> Create(CreateInvoiceRequest request)
        {
            var entity = _mapper.Map<Invoice>(request);
            await _repository.AddAsync(entity);
            return _mapper.Map<InvoiceResponse>(entity);
        }
        public async Task Delete(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception($"No invoice found with ID: {id}");
            await _repository.DeleteAsync(entity);
        }
    }
}
