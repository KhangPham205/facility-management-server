using backend.DTOs.Tang.Request;
using backend.DTOs.Tang.Response;

namespace backend.Services.TangService
{
    public interface ITangService
    {
        Task<TangResponse?> GetTangByIdAsync(string maTang);
        Task<IEnumerable<TangResponse>> GetAllTangAsync();
        Task<IEnumerable<TangResponse>> GetAllTangOfToaAsync(string maToa);
        Task<TangResponse?> CreateTangAsync(TangCreationRequest request);
        Task<TangResponse?> UpdateTangAsync(string maTang, TangUpdateRequest request);
        Task<bool> DeleteTangAsync(string maTang);
    }
}
