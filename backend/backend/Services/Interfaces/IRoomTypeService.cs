using backend.DTOs.RoomType.Request;
using backend.DTOs.RoomType.Response;
using backend.Models.Area;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IRoomTypeService
    {
        Task<PageVO<RoomTypeResponse>> GetAll(EntityFilter<RoomType> filter, EntitySort<RoomType> sort, int page, int size);
        Task<RoomTypeResponse?> GetById(string id);
        Task<RoomTypeResponse> Create(CreateRoomTypeRequest request);
        Task<RoomTypeResponse> Update(string id, CreateRoomTypeRequest request);
        Task Delete(string id);
    }
}