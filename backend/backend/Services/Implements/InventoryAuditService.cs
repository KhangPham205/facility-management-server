using AutoMapper;
using backend.DTOs.Audit.Request;
using backend.DTOs.Audit.Response;
using backend.Exceptions;
using backend.Models;
using backend.Models.Audit;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class InventoryAuditService : IInventoryAuditService
    {
        private readonly IInventoryAuditRepository _repo;
        private readonly IMapper _mapper;

        public InventoryAuditService(IInventoryAuditRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PageVO<InventoryAuditResponse>> GetAll(EntityFilter<InventoryAudit> filter, EntitySort<InventoryAudit> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<InventoryAuditResponse>>(pagedResult.Content);

            return new PageVO<InventoryAuditResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<InventoryAuditResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<InventoryAuditResponse>(entity);
        }

        public async Task<InventoryAuditResponse> Create(CreateInventoryAuditRequest request)
        {
            var entity = _mapper.Map<InventoryAudit>(request);

            await _repo.AddAsync(entity);
            return _mapper.Map<InventoryAuditResponse>(entity);
        }

        //public async Task<InventoryAuditResponse> Update(string id, UpdateInventoryAuditRequest request)
        //{
        //    var entity = await _repo.GetByIdAsync(id);
        //    if (entity == null) throw new NotFoundException($"No room found with ID: {id}");

        //    _mapper.Map(request, entity);

        //    await _repo.UpdateAsync(entity);
        //    return _mapper.Map<InventoryAuditResponse>(entity);
        //}

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No inventory audit found with ID: {id}");

            await _repo.DeleteAsync(entity);
        }
    }
}