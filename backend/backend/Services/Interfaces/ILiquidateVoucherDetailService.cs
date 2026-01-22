using backend.DTOs.Liquidate.Request;
using backend.DTOs.Liquidate.Response;
using backend.Enums;
using backend.Models.Liquidate;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface ILiquidateVoucherDetailService
    {
        Task<PageVO<LiquidateVoucherDetailResponse>> GetAll(EntityFilter<LiquidateVoucherDetail> filter, EntitySort<LiquidateVoucherDetail> sort, int page, int size);
        Task<LiquidateVoucherDetailResponse?> GetById(string voucherId, string equipmentId);
        //Task<LiquidateVoucherDetailResponse> Create(CreateLiquidateVoucherDetailVoucher request);
        //Task<LiquidateVoucherDetailResponse> Update(string id, UpdateLiquidateVoucherDetailVoucher request);
        //Task UpdateStatus(string id, UpdateLiquidateVoucherDetailStatusVoucher request);
        Task Delete(string voucherId, string equipmentId);
    }
}