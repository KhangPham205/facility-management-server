using AutoMapper;
using backend.DTOs.Floor.Request;
using backend.DTOs.Floor.Response;
using backend.Exceptions;
using backend.Models.Area;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class FloorService : IFloorService
    {
        private readonly IFloorRepository _repo;
        private readonly IMapper _mapper;

        public FloorService(IFloorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PageVO<FloorResponse>> GetAll(EntityFilter<Floor> filter, EntitySort<Floor> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<FloorResponse>>(pagedResult.Content);

            return new PageVO<FloorResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<FloorResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<FloorResponse>(entity);
        }

        public async Task<FloorResponse> Create(CreateFloorRequest request)
        {
            var entity = _mapper.Map<Floor>(request);

            await _repo.AddAsync(entity);
            return _mapper.Map<FloorResponse>(entity);
        }

        public async Task<FloorResponse> Update(string id, UpdateFloorRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No floor found with ID: {id}");

            _mapper.Map(request, entity);

            await _repo.UpdateAsync(entity);
            return _mapper.Map<FloorResponse>(entity);
        }

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No floor found with ID: {id}");

            await _repo.DeleteAsync(entity);
        }
    }
}