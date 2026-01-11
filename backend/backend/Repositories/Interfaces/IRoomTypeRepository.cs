using backend.Models.Area;

namespace backend.Repositories.Interfaces
{
    public interface IRoomTypeRepository
    {
        Task<RoomType?> GetByIdAsync(string id);
        Task<IEnumerable<RoomType>> GetAllAsync();
        Task AddAsync(RoomType entity);
        void Remove(RoomType entity);
        Task<bool> SaveChangesAsync();
    }
}

