using backend.DTOs.RoomType.Request;
using backend.DTOs.RoomType.Response;
using backend.Models.Area;

namespace backend.Mapping
{
    public static class RoomTypeMapper
    {
        public static RoomTypeResponse ToResponse(RoomType entity) => new()
        {
            RoomTypeId = entity.RoomTypeId,
            TypeName = entity.TypeName,
            Description = entity.Description
        };

        public static RoomType ToEntity(RoomTypeCreationRequest request) => new()
        {
            TypeName = request.TypeName,
            Description = request.Description
        };

        public static void UpdateEntity(RoomTypeUpdateRequest request, RoomType entity)
        {
            entity.TypeName = request.TypeName;
            entity.Description = request.Description;
        }
    }
}