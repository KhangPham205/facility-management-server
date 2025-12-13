using backend.Models;
using System.Numerics;

namespace backend.Repositories.Interfaces
{
    public interface ITangRepository
    {
        Task<Tang> getTangByIdAsync(string maTang);
        Task<IEnumerable<Tang>> getAllTangAsync();
        Task<IEnumerable<Tang>> getAllTangOfToaAsync(string maToa);
        Task AddTangAsync(Tang tang);
        Task RemoveTangAsync(Tang tang);
        Task<bool> SaveChangesAsync();
    }
}
