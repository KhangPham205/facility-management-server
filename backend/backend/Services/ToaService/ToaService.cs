using backend.DTOs.Toa.Request;
using backend.DTOs.Toa.Response;
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

        // *** Lấy tất cả ***
        public async Task<IEnumerable<ToaResponse>> GetAllToasAsync()
        {
            var toas = await _toaRepository.GetAllAsync();

            // Ánh xạ (Mapping) từ Entity/Model (Toa) sang DTO Response
            return toas.Select(t => new ToaResponse
            {
                maToa = t.maToa,
                tenToa = t.tenToa,
                soLuongTang = t.soLuongTang,
                ghiChu = t.ghiChu
            }).ToList();
        }

        // *** Lấy theo mã ***
        public async Task<ToaResponse?> GetToaByIdAsync(string maToa)
        {
            var toa = await _toaRepository.GetByIdAsync(maToa);

            if (toa == null)
            {
                return null;
            }

            // Ánh xạ (Mapping) từ Entity/Model (Toa) sang DTO Response
            return new ToaResponse
            {
                maToa = toa.maToa,
                tenToa = toa.tenToa,
                soLuongTang = toa.soLuongTang,
                ghiChu = toa.ghiChu
            };
        }

        // *** Tạo mới ***
        public async Task<ToaResponse> CreateToaAsync(ToaCreationRequest request)
        {
            // 1. Ánh xạ (Mapping) từ DTO Request sang Entity/Model (Toa)
            var newToa = new Toa
            {
                maToa = Guid.NewGuid().ToString(), // Sinh mã Toa ở đây
                tenToa = request.tenToa,
                soLuongTang = request.soLuongTang,
                ghiChu = request.ghiChu
            };

            // 2. Gọi Repository để thêm vào DB
            await _toaRepository.AddAsync(newToa);
            await _toaRepository.SaveChangesAsync();

            // 3. Ánh xạ Entity/Model đã lưu sang DTO Response để trả về
            return new ToaResponse
            {
                maToa = newToa.maToa,
                tenToa = newToa.tenToa,
                soLuongTang = newToa.soLuongTang,
                ghiChu = newToa.ghiChu
            };
        }

        //*** Cập nhật ***
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

            toa.tenToa = request.tenToa;
            toa.soLuongTang = request.soLuongTang;
            toa.ghiChu = request.ghiChu;

            var success = await _toaRepository.SaveChangesAsync();

            if (!success)
            {
                return null;
            }

            return new ToaResponse
            {
                maToa = maToa,
                tenToa = toa.tenToa,
                soLuongTang = toa.soLuongTang,
                ghiChu = toa.ghiChu
            };
        }

        //*** Xóa ***
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
