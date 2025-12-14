using backend.DTOs.Tang.Request;
using backend.DTOs.Tang.Response;
using backend.Mapping;
using backend.Models;
using backend.Repositories.Interfaces;

namespace backend.Services.TangService
{
    public class TangService : ITangService
    {
        private readonly ITangRepository _tangRepository;

        public TangService(ITangRepository tangRepository)
        {
            _tangRepository = tangRepository;
        }

        // GET TANG BY ID
        public async Task<TangResponse?> GetTangByIdAsync(string maTang){
            var tang = await _tangRepository.getTangByIdAsync(maTang);

            if(tang == null)
            {
                return null;
            }

            return TangMapper.TangResponseFromEntity(tang);
        }

        // GET ALL TANG
        public async Task<IEnumerable<TangResponse>> GetAllTangAsync()
        {
            var tangs = await _tangRepository.getAllTangAsync();

            return tangs.Select(tang => TangMapper.TangResponseFromEntity(tang)).ToList();

        }

        // GET ALL TANG OF TOA
        public async Task<IEnumerable<TangResponse>> GetAllTangOfToaAsync(string maToa)
        {
            var tangs = await _tangRepository.getAllTangOfToaAsync(maToa);

            return tangs.Select(tang => TangMapper.TangResponseFromEntity(tang)).ToList();
        }

        // CREATE TANG
        public async Task<TangResponse?> CreateTangAsync(TangCreationRequest request)
        {
            var newTang = TangMapper.EntityFromCreateRequest(request);
            newTang.maTang = Guid.NewGuid().ToString();

            await _tangRepository.AddTangAsync(newTang);
            bool isSuccessed = await _tangRepository.SaveChangesAsync();

            if (!isSuccessed)
            {
                return null;
            }

            return TangMapper.TangResponseFromEntity(newTang);
            
        }

        // UPDATE TANG
        public async Task<TangResponse?> UpdateTangAsync(string maTang, TangUpdateRequest request)
        {
            if (maTang  == null)
            {
                return null; 
            }

            var tang = await _tangRepository.getTangByIdAsync(maTang);

            if(tang == null)
            {
                return null;
            }

            TangMapper.EntityFromUpdateRequest(request, tang);

            bool isSuccessed = await _tangRepository.SaveChangesAsync();

            if (!isSuccessed)
            {
                return null;
            }

            return TangMapper.TangResponseFromEntity(tang);
        }

        // DELETE TANG
        public async Task<bool> DeleteTangAsync(string maTang)
        {
            if (maTang == null)
            {
                return false; 
            }

            var tang = await _tangRepository.getTangByIdAsync(maTang);

            await _tangRepository.RemoveTangAsync(tang);

            return await _tangRepository.SaveChangesAsync();
        }
    }
}
