using AutoMapper;
using backend.DTOs.Building.Request;
using backend.DTOs.Building.Response;
using backend.Exceptions;
using backend.Models.Area;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _repo;
        private readonly IMapper _mapper;

        public BuildingService(IBuildingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PageVO<BuildingResponse>> GetAll(EntityFilter<Building> filter, EntitySort<Building> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<BuildingResponse>>(pagedResult.Content);

            return new PageVO<BuildingResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<BuildingResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<BuildingResponse>(entity);
        }

        public async Task<BuildingResponse> Create(CreateBuildingRequest request)
        {
            var entity = _mapper.Map<Building>(request);

            await _repo.AddAsync(entity);
            return _mapper.Map<BuildingResponse>(entity);
        }

        public async Task<BuildingResponse> Update(string id, UpdateBuildingRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No building found with ID: {id}");

            _mapper.Map(request, entity);

            await _repo.UpdateAsync(entity);
            return _mapper.Map<BuildingResponse>(entity);
        }

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No building found with ID: {id}");

            await _repo.DeleteAsync(entity);
        }
    }
}