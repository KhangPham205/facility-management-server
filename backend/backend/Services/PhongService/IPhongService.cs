using backend.DTOs.Phong.Request;
using backend.DTOs.Phong.Response;

namespace backend.Services.PhongService
{
    public interface IPhongService
    {
        Task<PhongResponse?> GetPhongByIdAsync(string maPhong);
        Task<IEnumerable<PhongResponse>> GetAllPhongAsync();
        Task<IEnumerable<PhongResponse>> GetAllPhongOfToaAsync(string maToa);
        Task<IEnumerable<PhongResponse>> GetAllPhongOfTangAsync(string maTang);
        Task<PhongResponse?> CreatePhongAsync(PhongCreationRequest request);
        Task<PhongResponse?> UpdatePhongAsync(string maPhong, PhongUpdateRequest request);
        Task<bool> DeletePhongAsync(string maPhong);
    }
}
