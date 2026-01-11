using backend.DTOs.Area.Building.Request;
using backend.DTOs.Area.Building.Response;

namespace backend.Services.Interfaces
{
    public interface IBuildingService
    {
        Task<BuildingResponse> CreateBuildingAsync(BuildingCreationRequest request);
        Task<IEnumerable<BuildingResponse>> GetAllBuildingsAsync();
        Task<BuildingResponse?> GetBuildingByIdAsync(string id);
        Task<BuildingResponse?> UpdateBuildingAsync(string id, BuildingUpdateRequest request);
        Task<bool> DeleteBuildingAsync(string id);
    }
}