using backend.DTOs.Room.Request;
using backend.DTOs.Room.Response;
using backend.Models.Area;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IRoomService
    {
        Task<PageVO<RoomResponse>> GetAll(EntityFilter<Room> filter, EntitySort<Room> sort, int page, int size);
        Task<RoomResponse?> GetById(string id);
        Task<RoomResponse> Create(CreateRoomRequest request);
        Task<RoomResponse> Update(string id, UpdateRoomRequest request);
        Task Delete(string id);
    }
}