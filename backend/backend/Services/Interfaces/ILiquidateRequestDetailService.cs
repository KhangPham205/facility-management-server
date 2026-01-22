using backend.DTOs.Liquidate.Request;
using backend.DTOs.Liquidate.Response;
using backend.Enums;
using backend.Models.Liquidate;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface ILiquidateRequestDetailService
    {
        Task<PageVO<LiquidateRequestDetailResponse>> GetAll(EntityFilter<LiquidateRequestDetail> filter, EntitySort<LiquidateRequestDetail> sort, int page, int size);
        Task<LiquidateRequestDetailResponse?> GetById(string requestId, string equipmentId);
        //Task<LiquidateRequestDetailResponse> Create(CreateLiquidateRequestDetailRequest request);
        //Task<LiquidateRequestDetailResponse> Update(string id, UpdateLiquidateRequestDetailRequest request);
        //Task UpdateStatus(string id, UpdateLiquidateRequestDetailStatusRequest request);
        Task Delete(string requestId, string equipmentId);
    }
}