using backend.DTOs.Tang.Request;
using backend.DTOs.Tang.Response;
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

        public async Task<TangResponse?> GetTangByIdAsync(string maTang){
            var tang = await _tangRepository.getTangByIdAsync(maTang);

            if(tang == null)
            {
                return null;
            }

            return new TangResponse
            {
                maTang = maTang,
                maToa = tang.maToa,
                tenTang = tang.tenTang,
                ghiChu = tang.ghiChu,
            };
        }

        public async Task<IEnumerable<TangResponse>> GetAllTangAsync()
        {
            var tangs = await _tangRepository.getAllTangAsync();

            return tangs.Select(tang => new TangResponse
            {
                maTang = tang.maTang,
                maToa = tang.maToa,
                tenTang = tang.tenTang,
                ghiChu = tang.ghiChu,
            }).ToList();

        }

        public async Task<IEnumerable<TangResponse>> GetAllTangOfToaAsync(string maToa)
        {
            var tangs = await _tangRepository.getAllTangOfToaAsync(maToa);

            return tangs.Select(tang => new TangResponse
            {
                maTang = tang.maTang,
                maToa = tang.maToa,
                tenTang = tang.tenTang,
                ghiChu = tang.ghiChu,
            }).ToList();
        }

        public async Task<TangResponse?> CreateTangAsync(TangCreationRequest request)
        {
            var newTang = new Tang
            {
                maTang = Guid.NewGuid().ToString(),
                maToa = request.maToa,
                tenTang = request.tenTang,
                ghiChu = request.ghiChu,
            };

            await _tangRepository.AddTangAsync(newTang);
            bool isSuccessed = await _tangRepository.SaveChangesAsync();

            if (!isSuccessed)
            {
                return null;
            }

            return new TangResponse
            {
                maTang = newTang.maTang,
                maToa = newTang.maToa,
                tenTang = newTang.tenTang,
                ghiChu = newTang.ghiChu,
            };
            
        }
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

            tang.maToa = request.maToa;
            tang.tenTang = request.tenTang;
            tang.ghiChu = request.ghiChu;

            bool isSuccessed = await _tangRepository.SaveChangesAsync();

            if (!isSuccessed)
            {
                return null;
            }

            return new TangResponse
            {
                maTang = tang.maTang,
                maToa = tang.maToa,
                tenTang = tang.tenTang,
                ghiChu = tang.ghiChu,
            };
        }
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
