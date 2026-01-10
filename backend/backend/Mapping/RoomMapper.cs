using backend.DTOs.Room.Request;
using backend.DTOs.Room.Response;
using backend.Models;

namespace backend.Mapping
{
    public static class RoomMapper
    {
        public static RoomResponse ToResponse(Room entity) => new()
        {
            RoomId = entity.RoomId,
            FloorId = entity.FloorId,
            RoomName = entity.RoomName,
            RoomTypeId = entity.RoomTypeId,
            Capacity = entity.Capacity,
            Status = entity.Status,
            Note = entity.Note
        };

        public static Room ToEntity(RoomCreationRequest request) => new()
        {
            FloorId = request.FloorId,
            RoomName = request.RoomName,
            RoomTypeId = request.RoomTypeId,
            Capacity = request.Capacity,
            Status = request.Status,
            Note = request.Note
        };

        public static void UpdateEntity(RoomUpdateRequest request, Room entity)
        {
            entity.FloorId = request.FloorId;
            entity.RoomName = request.RoomName;
            entity.RoomTypeId = request.RoomTypeId;
            entity.Capacity = request.Capacity;
            entity.Status = request.Status;
            entity.Note = request.Note;
        }
    }
}