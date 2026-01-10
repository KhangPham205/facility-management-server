using backend.DTOs.Floor.Request;
using backend.DTOs.Floor.Response;

namespace backend.Services.Interfaces
{
    public interface IFloorService
    {
        Task<FloorResponse?> GetFloorByIdAsync(string floorId);
        Task<IEnumerable<FloorResponse>> GetAllFloorsAsync();
        Task<IEnumerable<FloorResponse>> GetFloorsByBuildingIdAsync(string buildingId);
        Task<FloorResponse?> CreateFloorAsync(FloorCreationRequest request);
        Task<FloorResponse?> UpdateFloorAsync(string floorId, FloorUpdateRequest request);
        Task<bool> DeleteFloorAsync(string floorId);
    }
}