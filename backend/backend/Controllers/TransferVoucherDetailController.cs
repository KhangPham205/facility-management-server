using backend.Constants;
using backend.DTOs.Transfer.Response;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Transfer
{
    // Route dạng lồng: /api/v1/transfer-vouchers/{voucherId}/details
    [Route(ApiEndpoints.TransferVouchers + "/{voucherId}/details")]
    [ApiController]
    public class TransferVoucherDetailController : ControllerBase
    {
        private readonly ITransferService _service;

        public TransferVoucherDetailController(ITransferService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransferVoucherDetailResponse>>> GetDetails(string voucherId)
        {
            // Lấy Voucher cha rồi trả về list Details
            var voucher = await _service.GetVoucherById(voucherId);
            return Ok(voucher.Details);
        }
    }
}