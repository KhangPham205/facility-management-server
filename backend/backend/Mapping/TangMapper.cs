using backend.DTOs.Tang.Request;
using backend.DTOs.Tang.Response;
using backend.Models;

namespace backend.Mapping
{
    public static class TangMapper
    {
        public static TangResponse TangResponseFromEntity(Tang tang)
        {
            return new TangResponse
            {
                maTang = tang.maTang,
                maToa = tang.maToa,
                tenTang = tang.tenTang,
                ghiChu = tang.ghiChu,
            };
        }

        public static Tang EntityFromCreateRequest(TangCreationRequest request)
        {
            return new Tang
            {
                maToa = request.maToa,
                tenTang = request.tenTang,
                ghiChu = request.ghiChu,
            };
        }

        public static void EntityFromUpdateRequest(TangUpdateRequest request, Tang tang)
        {
            tang.maToa = request.maToa;
            tang.tenTang = request.tenTang;
            tang.ghiChu = request.ghiChu;
        }
    }
}
