using backend.DTOs.Phong.Request;
using backend.Repositories.Interfaces;
using backend.DTOs.Phong.Response;
using backend.Mapping;

namespace backend.Services.PhongService
{
    public class PhongService : IPhongService
    {
        private readonly IPhongRepository _phongRepository;

        public PhongService(IPhongRepository phongRepository)
        {
            _phongRepository = phongRepository;
        }

        // GET PHONG BY ID
        public async Task<PhongResponse?> GetPhongByIdAsync(string maPhong)
        {
            if (String.IsNullOrWhiteSpace(maPhong))
                return null;

            var phong = await _phongRepository.GetPhongByIdAsync(maPhong);

            return PhongMapper.PhongResponseFromEntity(phong);
        }

        // GET ALL PHONG
        public async Task<IEnumerable<PhongResponse>> GetAllPhongAsync()
        {
            var phongs = await _phongRepository.GetAllPhongAsync();

            return phongs
                .Select(ph => PhongMapper.PhongResponseFromEntity(ph))
                .ToList();
        }

        // GET ALL PHONG OF TOA
        public async Task<IEnumerable<PhongResponse>> GetAllPhongOfToaAsync(string maToa)
        {
            var phongs = await _phongRepository.GetAllPhongOfToaAsync(maToa);

            return phongs
                .Select(ph => PhongMapper.PhongResponseFromEntity(ph))
                .ToList();
        }

        // GET ALL PHONG OF TANG
        public async Task<IEnumerable<PhongResponse>> GetAllPhongOfTangAsync(string maTang)
        {
            var phongs = await _phongRepository.GetAllPhongOfTangAsync(maTang);

            return phongs
                .Select(ph => PhongMapper.PhongResponseFromEntity(ph))
                .ToList();
        }

        // CREATE PHONG
        public async Task<PhongResponse?> CreatePhongAsync(PhongCreationRequest request)
        {
            if (request == null)
                return null;

            var phong = PhongMapper.EntityFromCreationRequest(request);
            phong.maPhong = Guid.NewGuid().ToString();

            await _phongRepository.AddPhongAsync(phong);
            bool isSuccessed = await _phongRepository.SaveChangesAsync();

            if (!isSuccessed)
                return null;

            return PhongMapper.PhongResponseFromEntity(phong);
        }

        // UPDATE PHONG
        public async Task<PhongResponse?> UpdatePhongAsync(string maPhong, PhongUpdateRequest request)
        {
            if (maPhong == null || request == null)
                return null;

            var phong = await _phongRepository.GetPhongByIdAsync(maPhong);
            PhongMapper.EntityFromUpdateRequest(request, phong);

            bool isSuccessed = await _phongRepository.SaveChangesAsync();

            if (!isSuccessed) return null;

            return PhongMapper.PhongResponseFromEntity(phong);
        }

        // DELETE PHONG
        public async Task<bool> DeletePhongAsync(string maPhong)
        {
            if (maPhong == null)
                return false;

            var phong = await _phongRepository.GetPhongByIdAsync(maPhong);

            if (phong == null) 
                return false;

            await _phongRepository.RemovePhongAsync(phong);
            bool isSuccessed = await _phongRepository.SaveChangesAsync();

            if (!isSuccessed) 
                return false;

            return true;
        }
    }
}
