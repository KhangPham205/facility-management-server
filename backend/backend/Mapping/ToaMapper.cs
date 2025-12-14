using backend.DTOs.Toa.Request;
using backend.DTOs.Toa.Response;
using backend.Models;

namespace backend.Mapping
{
    public static class ToaMapper
    {
        public static ToaResponse ToaResponseFromEntity(Toa toa)
        {
            return new ToaResponse
            {
                maToa = toa.maToa,
                tenToa = toa.tenToa,
                soLuongTang = toa.soLuongTang,
                ghiChu = toa.ghiChu
            };
        }

        public static Toa EntityFromCreateRequest(ToaCreationRequest request)
        {
            return new Toa
            {
                tenToa = request.tenToa,
                soLuongTang = request.soLuongTang,
                ghiChu = request.ghiChu
            };
        }

        public static void EntityFromUpdateRequest(ToaUpdateRequest request, Toa toa)
        {
            toa.tenToa = request.tenToa;
            toa.soLuongTang = request.soLuongTang;
            toa.ghiChu = request.ghiChu;
        }
    }
}
