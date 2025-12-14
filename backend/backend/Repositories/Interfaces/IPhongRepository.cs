using backend.Models.Phong;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Interfaces
{
    public interface IPhongRepository
    {
        Task<Phong?> GetPhongByIdAsync(string maTang);

        Task<IEnumerable<Phong>> GetAllPhongAsync();

        Task<IEnumerable<Phong>> GetAllPhongOfToaAsync(string maToa);

        Task<IEnumerable<Phong>> GetAllPhongOfTangAsync(string maTang);

        Task AddPhongAsync(Phong phong);

        Task RemovePhongAsync(Phong phong);

        Task<bool> SaveChangesAsync();
    }
}
