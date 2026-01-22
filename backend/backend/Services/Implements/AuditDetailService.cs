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
    public class AuditDetailService : IAuditDetailService
    {
        private readonly IAuditDetailRepository _repo;
        private readonly IMapper _mapper;

        public AuditDetailService(IAuditDetailRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PageVO<AuditDetailResponse>> GetAll(EntityFilter<AuditDetail> filter, EntitySort<AuditDetail> sort, int page, int size, string auditId)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size, auditId);

            var dtoList = _mapper.Map<List<AuditDetailResponse>>(pagedResult.Content);

            return new PageVO<AuditDetailResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<AuditDetailResponse?> GetById(string auditId, string equipmentId)
        {
            var entity = await _repo.GetByIdAsync(auditId, equipmentId);
            if (entity == null) return null;
            return _mapper.Map<AuditDetailResponse>(entity);
        }

        public async Task<AuditDetailResponse> Create(string auditId, string equipmentId, CreateAuditDetailRequest request)
        {
            var entity = _mapper.Map<AuditDetail>(request);
            entity.AuditId = auditId;
            entity.EquipmentId = equipmentId;

            await _repo.AddAsync(entity);
            return _mapper.Map<AuditDetailResponse>(entity);
        }

        public async Task<AuditDetailResponse> Update(string auditId, string equipmentId, UpdateAuditDetailRequest request)
        {
            var entity = await _repo.GetByIdAsync(auditId, equipmentId);
            if (entity == null) throw new NotFoundException("Not found audit detail");

            _mapper.Map(request, entity);

            await _repo.UpdateAsync(entity);
            return _mapper.Map<AuditDetailResponse>(entity);
        }

        public async Task Delete(string auditId, string equipmentId)
        {
            var entity = await _repo.GetByIdAsync(auditId, equipmentId);
            if (entity == null) throw new NotFoundException($"No periodic audit found with ID");

            await _repo.DeleteAsync(entity);
        }
    }
}