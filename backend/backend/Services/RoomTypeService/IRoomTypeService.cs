using backend.DTOs.RoomType.Request;
using backend.DTOs.RoomType.Response;

namespace backend.Services.RoomTypeService
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