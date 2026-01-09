using backend.DTOs.RoomType.Request;
using backend.DTOs.RoomType.Response;
using backend.Mapping;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services.Implements
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _repo;

        public RoomTypeService(IRoomTypeRepository repo)
        {
            _repo = repo;
        }

        public async Task<RoomTypeResponse?> GetByIdAsync(string roomTypeId)
        {
            var entity = await _repo.GetByIdAsync(roomTypeId);
            return entity == null ? null : RoomTypeMapper.ToResponse(entity);
        }

        public async Task<IEnumerable<RoomTypeResponse>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities.Select(RoomTypeMapper.ToResponse);
        }

        public async Task<RoomTypeResponse?> CreateAsync(RoomTypeCreationRequest request)
        {
            var entity = RoomTypeMapper.ToEntity(request);
            await _repo.AddAsync(entity);

            if (await _repo.SaveChangesAsync())
                return RoomTypeMapper.ToResponse(entity);

            return null;
        }

        public async Task<RoomTypeResponse?> UpdateAsync(string roomTypeId, RoomTypeUpdateRequest request)
        {
            var entity = await _repo.GetByIdAsync(roomTypeId);
            if (entity == null) return null;

            RoomTypeMapper.UpdateEntity(request, entity);

            if (await _repo.SaveChangesAsync())
                return RoomTypeMapper.ToResponse(entity);

            return null;
        }

        public async Task<bool> DeleteAsync(string roomTypeId)
        {
            var entity = await _repo.GetByIdAsync(roomTypeId);
            if (entity == null) return false;

            _repo.Remove(entity);
            return await _repo.SaveChangesAsync();
        }
    }
}