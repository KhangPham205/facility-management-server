using backend.DTOs.Area.Building.Request;
using backend.DTOs.Area.Building.Response;
using backend.Models.Area;

namespace backend.Mapping
{
    public static class BuildingMapper
    {
        // Chuyển từ Entity sang Response DTO
        public static BuildingResponse ToBuildingResponse(Building building)
        {
            return new BuildingResponse
            {
                BuildingId = building.BuildingId, // maToa
                BuildingName = building.BuildingName,             // tenToa
                FloorCount = building.FloorCount, // soLuongTang
                Note = building.Note              // ghiChu
            };
        }

        // Tạo Entity mới từ Create Request DTO
        public static Building ToEntityFromCreateRequest(BuildingCreationRequest request)
        {
            return new Building
            {
                BuildingName = request.BuildingName,
                FloorCount = request.FloorCount,
                Note = request.Note
            };
        }

        // Cập nhật Entity hiện có từ Update Request DTO
        public static void UpdateEntityFromRequest(BuildingUpdateRequest request, Building building)
        {
            building.BuildingName = request.BuildingName;
            building.FloorCount = request.FloorCount;
            building.Note = request.Note;
        }
    }
}