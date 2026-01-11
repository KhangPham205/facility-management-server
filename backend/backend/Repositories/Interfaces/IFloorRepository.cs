using backend.Models.Area;

namespace backend.Repositories.Interfaces
{
    public interface IFloorRepository
    {
        Task<Floor?> GetFloorByIdAsync(string floorId);
        Task<IEnumerable<Floor>> GetAllFloorsAsync();
        Task<IEnumerable<Floor>> GetFloorsByBuildingIdAsync(string buildingId);
        Task AddFloorAsync(Floor floor);
        Task RemoveFloorAsync(Floor floor);
        Task<bool> SaveChangesAsync();
    }
}