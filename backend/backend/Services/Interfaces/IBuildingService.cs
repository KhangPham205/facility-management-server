using backend.DTOs.Building.Request;
using backend.DTOs.Building.Response;
using backend.Models.Area;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IBuildingService
    {
        Task<PageVO<BuildingResponse>> GetAll(EntityFilter<Building> filter, EntitySort<Building> sort, int page, int size);
        Task<BuildingResponse?> GetById(string id);
        Task<BuildingResponse> Create(CreateBuildingRequest request);
        Task<BuildingResponse> Update(string id, UpdateBuildingRequest request);
        Task Delete(string id);
    }
}