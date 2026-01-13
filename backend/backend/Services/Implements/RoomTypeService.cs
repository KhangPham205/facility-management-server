using AutoMapper;
using backend.DTOs.RoomType.Request;
using backend.DTOs.RoomType.Response;
using backend.Exceptions;
using backend.Models.Area;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _repo;
        private readonly IMapper _mapper;

        public RoomTypeService(IRoomTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PageVO<RoomTypeResponse>> GetAll(EntityFilter<RoomType> filter, EntitySort<RoomType> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<RoomTypeResponse>>(pagedResult.Content);

            return new PageVO<RoomTypeResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<RoomTypeResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<RoomTypeResponse>(entity);
        }

        public async Task<RoomTypeResponse> Create(CreateRoomTypeRequest request)
        {
            var entity = _mapper.Map<RoomType>(request);

            await _repo.AddAsync(entity);
            return _mapper.Map<RoomTypeResponse>(entity);
        }

        public async Task<RoomTypeResponse> Update(string id, CreateRoomTypeRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No building found with ID: {id}");

            _mapper.Map(request, entity);

            await _repo.UpdateAsync(entity);
            return _mapper.Map<RoomTypeResponse>(entity);
        }

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No building found with ID: {id}");

            await _repo.DeleteAsync(entity);
        }
    }
}