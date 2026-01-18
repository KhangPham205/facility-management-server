using backend.DTOs.Maintenance.Request;
using backend.DTOs.Maintenance.Response;
using backend.Models.Maintenance;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IMaintenanceService
    {
        // Request
        Task<PageVO<MaintenanceRequestResponse>> GetRequests(EntityFilter<MaintenanceRequest> filter, EntitySort<MaintenanceRequest> sort, int page, int size);
        Task<MaintenanceRequestResponse> CreateRequest(CreateMaintenanceRequestRequest request);
        Task<MaintenanceRequestResponse> ApproveRequest(string requestId, UpdateMaintenanceRequestStatusRequest request);
        // Voucher
        Task<PageVO<MaintenanceVoucherResponse>> GetVouchers(EntityFilter<MaintenanceVoucher> filter, EntitySort<MaintenanceVoucher> sort, int page, int size);
        Task<MaintenanceVoucherResponse> CreateVoucher(CreateMaintenanceVoucherRequest request);
    }
}
