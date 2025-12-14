using backend.DTOs.Toa.Request;
using backend.DTOs.Toa.Response;
using backend.Mapping;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.ToaService
{
    public class ToaService : IToaService
    {
        private readonly IToaRepository _toaRepository;

        public ToaService(IToaRepository toaRepository)
        {
            _toaRepository = toaRepository;
        }

        // GET ALL TOA
        public async Task<IEnumerable<ToaResponse>> GetAllToasAsync()
        {
            var toas = await _toaRepository.GetAllAsync();

            return toas.Select(t => ToaMapper.ToaResponseFromEntity(t)).ToList();
        }

        // GET TOA BY ID
        public async Task<ToaResponse?> GetToaByIdAsync(string maToa)
        {
            var toa = await _toaRepository.GetByIdAsync(maToa);

            if (toa == null)
            {
                return null;
            }

            return ToaMapper.ToaResponseFromEntity(toa);
        }

        // CREATE TOA
        public async Task<ToaResponse> CreateToaAsync(ToaCreationRequest request)
        {
            var newToa = ToaMapper.EntityFromCreateRequest(request);

            newToa.maToa = Guid.NewGuid().ToString();

            await _toaRepository.AddAsync(newToa);
            await _toaRepository.SaveChangesAsync();

            return ToaMapper.ToaResponseFromEntity(newToa);
        }

        // UPDATE TOA
        public async Task<ToaResponse?> UpdateToaAsync(string maToa, ToaUpdateRequest request)
        {
            if (maToa == null)
            {
                return null;
            }

            Toa toa = await _toaRepository.GetByIdAsync(maToa);

            if(toa == null)
            {
                return null;
            }

            ToaMapper.EntityFromUpdateRequest(request, toa);

            var success = await _toaRepository.SaveChangesAsync();

            if (!success)
            {
                return null;
            }

            return ToaMapper.ToaResponseFromEntity(toa);
        }

        // DELETE TOA
        public async Task<bool> DeleteToaAsync(string maToa)
        {
            var toa = await _toaRepository.GetByIdAsync(maToa);

            if (toa == null)
            {
                return false;
            }

            await _toaRepository.DeleteAsync(toa);

            return await _toaRepository.SaveChangesAsync();
        }
    }
}
