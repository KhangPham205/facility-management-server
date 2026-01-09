using backend.DTOs.Building.Request;
using backend.DTOs.Building.Response;

namespace backend.Services.BuildingService
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