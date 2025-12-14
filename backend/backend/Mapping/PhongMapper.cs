using Azure.Core;
using backend.DTOs.Phong.Request;
using backend.DTOs.Phong.Response;
using backend.Models.Phong;

namespace backend.Mapping
{
    public static class PhongMapper
    {
        public static Phong EntityFromCreationRequest (PhongCreationRequest request)
        {
            return new Phong
            {
                maTang = request.maTang,
                tenPhong = request.tenPhong,
                maLoaiPhong = request.maLoaiPhong,
                sucChua = request.sucChua,
                tinhTrang = request.tinhTrang,
                ghiChu = request.ghiChu,
            };
        }

        public static PhongResponse PhongResponseFromEntity (Phong phong)
        {
            return new PhongResponse
            {
                maPhong = phong.maPhong,
                maTang = phong.maTang,
                tenPhong = phong.tenPhong,
                maLoaiPhong = phong.maLoaiPhong,
                sucChua = phong.sucChua,
                tinhTrang = phong.tinhTrang,
                ghiChu = phong.ghiChu,
            };
        }

        public static void EntityFromUpdateRequest(PhongUpdateRequest request, Phong phong)
        {
            phong.maTang = request.maTang;
            phong.tenPhong = request.tenPhong;
            phong.maLoaiPhong = request.maLoaiPhong;
            phong.sucChua = request.sucChua;
            phong.tinhTrang = request.tinhTrang;
            phong.ghiChu = request.ghiChu;
        }
    }
}
