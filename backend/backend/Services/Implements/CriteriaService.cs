using AutoMapper;
using backend.DTOs.Criteria.Request;
using backend.DTOs.Criteria.Response;
using backend.Exceptions;
using backend.Models.EquipmentInfo;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class CriteriaService : ICriteriaService
    {
        private readonly ICriteriaRepository _repo;
        private readonly IMapper _mapper;

        public CriteriaService(ICriteriaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PageVO<CriteriaResponse>> GetAll(EntityFilter<Criteria> filter, EntitySort<Criteria> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<CriteriaResponse>>(pagedResult.Content);

            return new PageVO<CriteriaResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<CriteriaResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<CriteriaResponse>(entity);
        }

        public async Task<CriteriaResponse> Create(CreateCriteriaRequest request)
        {
            var entity = _mapper.Map<Criteria>(request);

            await _repo.AddAsync(entity);
            return _mapper.Map<CriteriaResponse>(entity);
        }

        public async Task<List<CriteriaResponse>> CreateList(CreateCriteriaListRequest request)
        {
            var entities = request.Contents.Select(content => new Criteria
            {
                CategoryId = request.CategoryId,
                Content = content.Content
            }).ToList();

            await _repo.AddRangeAsync(entities);

            return _mapper.Map<List<CriteriaResponse>>(entities);
        }

        public async Task<CriteriaResponse> Update(string id, UpdateCriteriaRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No criteria found with ID: {id}");

            _mapper.Map(request, entity);

            await _repo.UpdateAsync(entity);
            return _mapper.Map<CriteriaResponse>(entity);
        }

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No criteria found with ID: {id}");

            await _repo.DeleteAsync(entity);
        }
    }
}