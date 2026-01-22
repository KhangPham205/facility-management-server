using backend.Enums;
using backend.Models.Area;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IAreaRepository
    {
        Task<String?> GetNameByIdAsync(LocationType? type, string id);

        Task<Dictionary<string, string>> GetNamesByIdsAsync(LocationType? type, List<string> ids);
    }
}