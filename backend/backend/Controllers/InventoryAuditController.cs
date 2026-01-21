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
    [Route(ApiEndpoints.InventoryAudits)]
    [ApiController]
    public class InventoryAuditController : ControllerBase
    {
        private readonly IInventoryAuditService _service;

        public InventoryAuditController(IInventoryAuditService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<InventoryAuditResponse>>> GetAll(
            [FromQuery] EntityFilter<InventoryAudit> filter,
            [FromQuery] EntitySort<InventoryAudit> orderBy,
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
        public async Task<ActionResult<InventoryAuditResponse>> GetById(string id)
        {
            var result = await _service.GetById(id);
            if (result == null) return NotFound(new { message = "Không tìm thấy phiếu kiểm kê." });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryAuditResponse>> Create([FromBody] CreateInventoryAuditRequest request)
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
        //public async Task<ActionResult<InventoryAuditResponse>> Update(string id, [FromBody] UpdateInventoryAuditRequest request)
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