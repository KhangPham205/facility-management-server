using backend.DTOs.Floor.Request;
using backend.DTOs.Floor.Response;
using backend.Models.Area;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IFloorService
    {
        Task<PageVO<FloorResponse>> GetAll(EntityFilter<Floor> filter, EntitySort<Floor> sort, int page, int size);
        Task<FloorResponse?> GetById(string id);
        Task<FloorResponse> Create(CreateFloorRequest request);
        Task<FloorResponse> Update(string id, CreateFloorRequest request);
        Task Delete(string id);
    }
}