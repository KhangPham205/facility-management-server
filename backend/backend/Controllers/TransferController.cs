using backend.Constants;
using backend.DTOs.Transfer.Request;
using backend.DTOs.Transfer.Response;
using backend.Models.Transfer;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.TransferVouchers)]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _service;

        public TransferController(ITransferService service)
        {
            _service = service;
        }

        // --- REQUEST API ---

        [HttpGet("requests")]
        public async Task<ActionResult<PageVO<TransferRequestResponse>>> GetRequests(
            [FromQuery] EntityFilter<TransferRequest> filter,
            [FromQuery] EntitySort<TransferRequest> sort,
            [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            return Ok(await _service.GetRequests(filter, sort, page, size));
        }

        [HttpPost("requests")]
        public async Task<ActionResult<TransferRequestResponse>> CreateRequest([FromBody] CreateTransferRequestRequest request)
        {
            var result = await _service.CreateRequest(request);
            return Ok(result);
        }

        [HttpPut("requests/{id}/approve")]
        public async Task<ActionResult<TransferRequestResponse>> ApproveRequest(string id, [FromBody] ApproveTransferRequest request)
        {
            try
            {
                var result = await _service.ApproveRequest(id, request);
                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        // --- VOUCHER API ---

        [HttpGet("vouchers")]
        public async Task<ActionResult<PageVO<TransferVoucherResponse>>> GetVouchers(
            [FromQuery] EntityFilter<TransferVoucher> filter,
            [FromQuery] EntitySort<TransferVoucher> sort,
            [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            return Ok(await _service.GetVouchers(filter, sort, page, size));
        }

        [HttpPost("vouchers")] // Execute Transfer
        public async Task<ActionResult<TransferVoucherResponse>> CreateVoucher([FromBody] CreateTransferVoucherRequest request)
        {
            try
            {
                var result = await _service.CreateVoucher(request);
                return Created("", result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}