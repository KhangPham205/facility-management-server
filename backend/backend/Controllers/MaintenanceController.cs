using backend.Constants;
using backend.DTOs.Maintenance.Request;
using backend.Models.Maintenance;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.MaintenanceVouchers)]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _service;
        public MaintenanceController(IMaintenanceService service)
        {
            _service = service;
        }
        // --- REQUEST API ---
        [HttpGet("requests")]
        public async Task<IActionResult> GetRequests(
            [FromQuery] EntityFilter<MaintenanceRequest> filter,
            [FromQuery] EntitySort<MaintenanceRequest> orderBy,
            [FromQuery] int page = 1, 
            [FromQuery] int size = 10)
        {
            var result = await _service.GetRequests(filter, orderBy, page, size);
            return Ok(result);
        }

        [HttpPost("requests")]
        public async Task<IActionResult> CreateRequest([FromBody] CreateMaintenanceRequestRequest request)
        {
            var result = await _service.CreateRequest(request);
            return Ok(result);
        }

        [HttpPut("requests/{id}/approve")]
        public async Task<IActionResult> ApproveRequest(string id, [FromBody] UpdateMaintenanceRequestStatusRequest request)
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
        public async Task<IActionResult> GetVouchers(
            [FromQuery] EntityFilter<MaintenanceVoucher> filter,
            [FromQuery] EntitySort<MaintenanceVoucher> sort,
            [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _service.GetVouchers(filter, sort, page, size);
            return Ok(result);
        }

        [HttpPost("vouchers")]
        public async Task<IActionResult> CreateVoucher([FromBody] CreateMaintenanceVoucherRequest request)
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
