using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room?> GetByIdAsync(string roomId);
        Task<IEnumerable<Room>> GetAllAsync();
        Task<IEnumerable<Room>> GetByFloorIdAsync(string floorId);
        Task AddAsync(Room room);
        void Remove(Room room);
        Task<bool> SaveChangesAsync();
    }
}
