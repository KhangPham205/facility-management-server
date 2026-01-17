using backend.DTOs.Liquidate.Request;
using backend.DTOs.Liquidate.Response;
using backend.Enums;
using backend.Models;
using backend.Models.Liquidate;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface ILiquidateVoucherService
    {
        Task<PageVO<LiquidateVoucherResponse>> GetAll(EntityFilter<LiquidateVoucher> filter, EntitySort<LiquidateVoucher> sort, int page, int size);
        Task<LiquidateVoucherResponse?> GetById(string id);
        Task<LiquidateVoucherResponse> Create(CreateLiquidateVoucherRequest request);
        //Task<LiquidateVoucherResponse> Update(string id, UpdateLiquidateVoucherRequest request);
        Task Delete(string id);
    }
}