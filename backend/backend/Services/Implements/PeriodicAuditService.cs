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
    public class PeriodicAuditService : IPeriodicAuditService
    {
        private readonly IPeriodicAuditRepository _repo;
        private readonly IMapper _mapper;

        public PeriodicAuditService(IPeriodicAuditRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PageVO<PeriodicAuditResponse>> GetAll(EntityFilter<PeriodicAudit> filter, EntitySort<PeriodicAudit> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<PeriodicAuditResponse>>(pagedResult.Content);

            return new PageVO<PeriodicAuditResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<PeriodicAuditResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<PeriodicAuditResponse>(entity);
        }

        public async Task<PeriodicAuditResponse> Create(CreatePeriodicAuditRequest request)
        {
            var entity = _mapper.Map<PeriodicAudit>(request);

            await _repo.AddAsync(entity);
            return _mapper.Map<PeriodicAuditResponse>(entity);
        }

        //public async Task<PeriodicAuditResponse> Update(string id, UpdatePeriodicAuditRequest request)
        //{
        //    var entity = await _repo.GetByIdAsync(id);
        //    if (entity == null) throw new NotFoundException($"No room found with ID: {id}");

        //    _mapper.Map(request, entity);

        //    await _repo.UpdateAsync(entity);
        //    return _mapper.Map<PeriodicAuditResponse>(entity);
        //}

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No periodic audit found with ID: {id}");

            await _repo.DeleteAsync(entity);
        }
    }
}