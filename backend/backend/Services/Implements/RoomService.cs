using AutoMapper;
using backend.DTOs.Room.Request;
using backend.DTOs.Room.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models.Area;
using backend.Repositories.Implements;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repo;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PageVO<RoomResponse>> GetAll(EntityFilter<Room> filter, EntitySort<Room> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<RoomResponse>>(pagedResult.Content);

            return new PageVO<RoomResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<RoomResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<RoomResponse>(entity);
        }

        public async Task<RoomResponse> Create(CreateRoomRequest request)
        {
            var entity = _mapper.Map<Room>(request);

            await _repo.AddAsync(entity);
            return _mapper.Map<RoomResponse>(entity);
        }

        public async Task<RoomResponse> Update(string id, UpdateRoomRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No room found with ID: {id}");

            _mapper.Map(request, entity);

            await _repo.UpdateAsync(entity);
            return _mapper.Map<RoomResponse>(entity);
        }

        public async Task<bool> UpdateRoomStatusAsync(string roomId, UpdateRoomStatusRequest request)
        {
            if (string.IsNullOrEmpty(roomId))
            {
                throw new ArgumentException("ID phòng không được để trống.");
            }

            var isUpdated = await _repo.UpdateStatusAsync(roomId, request.Status);

            if (!isUpdated)
            {
                return false;
            }

            return true;
        }

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No room found with ID: {id}");

            await _repo.DeleteAsync(entity);
        }
    }
}