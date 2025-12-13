using backend.DTOs.Toa.Request;
using backend.DTOs.Toa.Response;

namespace backend.Services.ToaService
{
    public interface IToaService
    {
        Task<ToaResponse> CreateToaAsync(ToaCreationRequest request);
        Task<IEnumerable<ToaResponse>> GetAllToasAsync();
        Task<ToaResponse?> GetToaByIdAsync(string maToa);
        Task<ToaResponse?> UpdateToaAsync(string maToa, ToaUpdateRequest request);
        Task<bool> DeleteToaAsync(string maToa);
    }
}
