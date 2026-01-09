using backend.DTOs.Room.Request;
using backend.DTOs.Room.Response;
using backend.Mapping;
using backend.Repositories.Interfaces;

namespace backend.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repo;
        public RoomService(IRoomRepository repo) => _repo = repo;

        public async Task<RoomResponse?> GetByIdAsync(string roomId)
        {
            var entity = await _repo.GetByIdAsync(roomId);
            return entity == null ? null : RoomMapper.ToResponse(entity);
        }

        public async Task<IEnumerable<RoomResponse>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities.Select(RoomMapper.ToResponse);
        }

        public async Task<IEnumerable<RoomResponse>> GetByFloorIdAsync(string floorId)
        {
            var entities = await _repo.GetByFloorIdAsync(floorId);
            return entities.Select(RoomMapper.ToResponse);
        }

        public async Task<RoomResponse?> CreateAsync(RoomCreationRequest request)
        {
            var entity = RoomMapper.ToEntity(request);
            await _repo.AddAsync(entity);
            return await _repo.SaveChangesAsync() ? RoomMapper.ToResponse(entity) : null;
        }

        public async Task<RoomResponse?> UpdateAsync(string roomId, RoomUpdateRequest request)
        {
            var entity = await _repo.GetByIdAsync(roomId);
            if (entity == null) return null;
            RoomMapper.UpdateEntity(request, entity);
            return await _repo.SaveChangesAsync() ? RoomMapper.ToResponse(entity) : null;
        }

        public async Task<bool> DeleteAsync(string roomId)
        {
            var entity = await _repo.GetByIdAsync(roomId);
            if (entity == null) return false;
            _repo.Remove(entity);
            return await _repo.SaveChangesAsync();
        }
    }
}
