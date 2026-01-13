using backend.DTOs.Transfer.Request;
using backend.DTOs.Transfer.Response;
using backend.Models.Transfer;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface ITransferService
    {
        // Request
        Task<PageVO<TransferRequestResponse>> GetRequests(EntityFilter<TransferRequest> filter, EntitySort<TransferRequest> sort, int page, int size);
        Task<TransferRequestResponse> CreateRequest(CreateTransferRequestRequest request);
        Task<TransferRequestResponse> ApproveRequest(string requestId, ApproveTransferRequest request);

        // Voucher
        Task<PageVO<TransferVoucherResponse>> GetVouchers(EntityFilter<TransferVoucher> filter, EntitySort<TransferVoucher> sort, int page, int size);
        Task<TransferVoucherResponse> CreateVoucher(CreateTransferVoucherRequest request);
    }
}
