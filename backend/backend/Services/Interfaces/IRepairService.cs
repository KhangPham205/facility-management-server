using backend.DTOs.Repair.Request;
using backend.DTOs.Repair.Response;
using backend.Models.Repair;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IRepairService
    {
        // Request
        Task<PageVO<RepairRequestResponse>> GetRequests(EntityFilter<RepairRequest> filter, EntitySort<RepairRequest> sort, int page, int size);
        Task<RepairRequestResponse> GetRequestById(string requestId);
        Task<RepairRequestResponse> CreateRequest(CreateRepairRequestRequest request);
        Task<RepairRequestResponse> ApproveRequest(string requestId, UpdateRepairRequestStatusRequest request);
        // Voucher
        Task<PageVO<RepairVoucherResponse>> GetVouchers(EntityFilter<RepairVoucher> filter, EntitySort<RepairVoucher> sort, int page, int size);
        Task<RepairVoucherResponse> GetVoucherById(string voucherId);
        Task<RepairVoucherResponse> CreateVoucher(CreateRepairVoucherRequest request);
    }
}
