using backend.Constants;
using backend.DTOs.Transfer.Response;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Transfer
{
    // Route dạng lồng: /api/v1/transfer-requests/{requestId}/details
    [Route(ApiEndpoints.TransferRequests + "/{requestId}/details")]
    [ApiController]
    public class TransferRequestDetailController : ControllerBase
    {
        private readonly ITransferService _service;

        public TransferRequestDetailController(ITransferService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransferRequestDetailResponse>>> GetDetails(string requestId)
        {
            var request = await _service.GetRequestById(requestId);
            return Ok(request.Details);
        }

        // Sau này nếu có nghiệp vụ "Xóa 1 dòng chi tiết" hoặc "Sửa số lượng 1 dòng"
        // Bạn sẽ viết API DELETE/PUT tại đây.
    }
}