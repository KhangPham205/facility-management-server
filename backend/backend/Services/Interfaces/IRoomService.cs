using backend.DTOs.Room.Request;
using backend.DTOs.Room.Response;

namespace backend.Services.Interfaces
{
    public interface IRoomService
    {
        Task<RoomResponse?> GetByIdAsync(string roomId);
        Task<IEnumerable<RoomResponse>> GetAllAsync();
        Task<IEnumerable<RoomResponse>> GetByFloorIdAsync(string floorId);
        Task<RoomResponse?> CreateAsync(RoomCreationRequest request);
        Task<RoomResponse?> UpdateAsync(string roomId, RoomUpdateRequest request);
        Task<bool> DeleteAsync(string roomId);
    }
}
