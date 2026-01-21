using backend.Constants;
using backend.DTOs.Audit.Request;
using backend.DTOs.Audit.Response;
using backend.Models;
using backend.Models.Audit;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.PeriodicAudits)]
    [ApiController]
    public class PeriodicAuditController : ControllerBase
    {
        private readonly IPeriodicAuditService _service;

        public PeriodicAuditController(IPeriodicAuditService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<PeriodicAuditResponse>>> GetAll(
            [FromQuery] EntityFilter<PeriodicAudit> filter,
            [FromQuery] EntitySort<PeriodicAudit> orderBy,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            try
            {
                var result = await _service.GetAll(filter, orderBy, page, size);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PeriodicAuditResponse>> GetById(string id)
        {
            var result = await _service.GetById(id);
            if (result == null) return NotFound(new { message = "Không tìm thấy lịch kiểm kê định kỳ." });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PeriodicAuditResponse>> Create([FromBody] CreatePeriodicAuditRequest request)
        {
            try
            {
                var result = await _service.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = result.PeriodId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<PeriodicAuditResponse>> Update(string id, [FromBody] UpdatePeriodicAuditRequest request)
        //{
        //    try
        //    {
        //        var result = await _service.Update(id, request);
        //        return Ok(result);
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _service.Delete(id);
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