using backend.DTOs.Area.RoomType.Request;
using backend.DTOs.Area.RoomType.Response;

namespace backend.Services.Interfaces
{
    public interface IRoomTypeService
    {
        Task<RoomTypeResponse?> GetByIdAsync(string roomTypeId);
        Task<IEnumerable<RoomTypeResponse>> GetAllAsync();
        Task<RoomTypeResponse?> CreateAsync(RoomTypeCreationRequest request);
        Task<RoomTypeResponse?> UpdateAsync(string roomTypeId, RoomTypeUpdateRequest request);
        Task<bool> DeleteAsync(string roomTypeId);
    }
}