using backend.Constants;
using backend.DTOs.Audit.Request;
using backend.DTOs.Audit.Response;
using backend.Models;
using backend.Models.Audit;
using backend.Models.EquipmentInfo;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.AuditDetails)]
    [ApiController]
    public class AuditDetailController : ControllerBase
    {
        private readonly IAuditDetailService _service;

        public AuditDetailController(IAuditDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<AuditDetailResponse>>> GetPage(
            string auditId,
            [FromQuery] EntityFilter<AuditDetail> filter,
            [FromQuery] EntitySort<AuditDetail> orderBy, // Tự động map ?orderBy=Amount-desc
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            try
            {
                var result = await _service.GetAll(filter, orderBy, page, size, auditId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{auditId}/{equipmentId}")]
        public async Task<ActionResult<AuditDetailResponse>> GetById(string auditId, string equipmentId)
        {
            var result = await _service.GetById(auditId, equipmentId);
            if (result == null) return NotFound(new { message = "Không tìm thấy lịch kiểm kê định kỳ." });
            return Ok(result);
        }

        [HttpPost("{auditId}/{equipmentId}")]
        public async Task<ActionResult<AuditDetailResponse?>> Create(string auditId, string equipmentId, [FromBody] CreateAuditDetailRequest request)
        {
            try
            {
                return await _service.Create(auditId, equipmentId, request);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{auditId}/{equipmentId}")]
        public async Task<ActionResult<AuditDetailResponse>> Update(string auditId, string equipmentId, [FromBody] UpdateAuditDetailRequest request)
        {
            try
            {
                var result = await _service.Update(auditId, equipmentId, request);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{auditId}/{equipmentId}")]
        public async Task<IActionResult> Delete(string auditId, string equipmentId)
        {
            try
            {
                await _service.Delete(auditId, equipmentId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}