using backend.Constants;
using backend.DTOs.Repair.Request;
using backend.DTOs.Repair.Response;
using backend.Models.Repair;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.RepairVouchers)]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private readonly IRepairService _service;
        public RepairController(IRepairService service)
        {
            _service = service;
        }
        // --- REQUEST API ---
        [HttpGet("requests")]
        public async Task<ActionResult<PageVO<RepairRequestResponse>>> GetRequests(
            [FromQuery] EntityFilter<RepairRequest> filter,
            [FromQuery] EntitySort<RepairRequest> orderBy,
            [FromQuery] int page = 1, 
            [FromQuery] int size = 10)
        {
            return Ok(await _service.GetRequests(filter, orderBy, page, size));
        }

        [HttpPost("requests")]
        public async Task<ActionResult<RepairRequestResponse>> CreateRequest([FromBody] CreateRepairRequestRequest request)
        {
            var result = await _service.CreateRequest(request);
            return Ok();
        }
        [HttpPut("requests/{id}/approve")]
        public async Task<ActionResult<RepairRequestResponse>> ApproveRequest(string id, [FromBody] UpdateRepairRequestStatusRequest request)
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
        public async Task<ActionResult<PageVO<RepairVoucherResponse>>> GetVouchers(
            [FromQuery] EntityFilter<RepairVoucher> filter,
            [FromQuery] EntitySort<RepairVoucher> sort,
            [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            return Ok(await _service.GetVouchers(filter, sort, page, size));
        }

        [HttpPost("vouchers")]
        public async Task<ActionResult<RepairVoucherResponse>> CreateVoucher([FromBody] CreateRepairVoucherRequest request)
        {
            try
            {
                var result = await _service.CreateVoucher(request);
                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }
    }
}
