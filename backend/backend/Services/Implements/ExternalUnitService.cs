using AutoMapper;
using backend.Models.Finance;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using DTOs.ExternalUnit.Request;
using DTOs.ExternalUnit.Response;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class ExternalUnitService : IExternalUnitService
    {
        private readonly IExternalUnitRepository _repository;
        private readonly IMapper _mapper;
        public ExternalUnitService(IExternalUnitRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Delete(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception($"No external unit found with ID: {id}");

            await _repository.DeleteAsync(entity);
        }
        public async Task<ExternalUnitResponse> Create(CreateExternalUnitRequest request)
        {
            var entity = _mapper.Map<ExternalUnit>(request);

            await _repository.AddAsync(entity);
            return _mapper.Map<ExternalUnitResponse>(entity);
        }
        public async Task<PageVO<ExternalUnitResponse>> GetAll(EntityFilter<ExternalUnit> filter, EntitySort<ExternalUnit> sort, int page, int size)
        {
            var pagedResult = await _repository.GetPagedAsync(filter, sort, page, size);
            
            var dtoList = _mapper.Map<List<ExternalUnitResponse>>(pagedResult.Content);
            
            return new PageVO<ExternalUnitResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }
        public async Task<ExternalUnitResponse?> GetById(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<ExternalUnitResponse>(entity);
        }
        public async Task<ExternalUnitResponse> Update(string id, UpdateExternalUnitRequest request)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception($"No external unit found with ID: {id}");
            _mapper.Map(request, entity);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<ExternalUnitResponse>(entity);
        }
    }
}
