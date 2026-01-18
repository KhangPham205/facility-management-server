using backend.DTOs.Liquidate.Request;
using backend.DTOs.Liquidate.Response;
using backend.Enums;
using backend.Models.Liquidate;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface ILiquidateRequestService
    {
        Task<PageVO<LiquidateRequestResponse>> GetAll(EntityFilter<LiquidateRequest> filter, EntitySort<LiquidateRequest> sort, int page, int size);
        Task<LiquidateRequestResponse?> GetById(string id);
        Task<LiquidateRequestResponse> Create(CreateLiquidateRequestRequest request);
        //Task<LiquidateRequestResponse> Update(string id, UpdateLiquidateRequestRequest request);
        Task UpdateStatus(string id, UpdateLiquidateRequestStatusRequest request);
        Task Delete(string id);
    }
}