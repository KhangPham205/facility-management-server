using backend.DTOs.Floor.Request;
using backend.DTOs.Floor.Response;
using backend.Models;

namespace backend.Mapping
{
    public static class FloorMapper
    {
        // Chuyển đổi từ Entity sang Response DTO
        public static FloorResponse ToFloorResponse(Floor floor)
        {
            return new FloorResponse
            {
                FloorId = floor.FloorId,
                BuildingId = floor.BuildingId,
                FloorName = floor.FloorName,
                Note = floor.Note,
            };
        }

        // Chuyển đổi từ Create Request DTO sang Entity
        public static Floor ToFloorEntity(FloorCreationRequest request)
        {
            return new Floor
            {
                FloorId = request.FloorId,
                BuildingId = request.BuildingId,
                FloorName = request.FloorName,
                Note = request.Note,
            };
        }

        // Cập nhật dữ liệu từ Update Request DTO vào Entity hiện có
        public static void UpdateFloorFromRequest(FloorUpdateRequest request, Floor floor)
        {
            floor.BuildingId = request.BuildingId;
            floor.FloorName = request.FloorName;
            floor.Note = request.Note;
        }
    }
}