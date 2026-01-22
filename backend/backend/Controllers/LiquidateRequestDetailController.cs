using backend.Constants;
using backend.DTOs.Liquidate.Request;
using backend.DTOs.Liquidate.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models.Liquidate;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.LiquidateRequestDetails)]
    [ApiController]
    public class LiquidateRequestDetailController : ControllerBase
    {
        private readonly ILiquidateRequestDetailService _service;

        public LiquidateRequestDetailController(ILiquidateRequestDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<LiquidateRequestDetailResponse>>> GetAll(
            [FromQuery] EntityFilter<LiquidateRequestDetail> filter,
            [FromQuery] EntitySort<LiquidateRequestDetail> orderBy,
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
        public async Task<ActionResult<LiquidateRequestDetailResponse>> GetById(string requestId, string equipmentId)
        {
            if (string.IsNullOrEmpty(requestId) && string.IsNullOrEmpty(equipmentId))
                return BadRequest(new { message = "Thiếu equipment id"});

            var result = await _service.GetById(requestId, equipmentId);
            if (result == null) return NotFound(new { message = "Không tìm thấy thiết bị" });
            return Ok(result);
        }

        //[HttpPost]
        //public async Task<ActionResult<LiquidateRequestDetailResponse>> Create([FromBody] CreateLiquidateRequestDetailRequest request)
        //{
        //    try
        //    {
        //        var result = await _service.Create(request);
        //        return CreatedAtAction(nameof(GetById), new { id = result.RequestId }, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<LiquidateRequestDetailResponse>> Update(string id, [FromBody] UpdateLiquidateRequestDetailRequest request)
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

        //[HttpPatch("{id}/status")]
        //public async Task<IActionResult> UpdateStatus(string id, [FromBody] UpdateLiquidateRequestDetailStatusRequest request)
        //{
        //    try
        //    {
        //        await _service.UpdateStatus(id, request);
        //        return NoContent();
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string requestId, string equipmentId)
        {
            try
            {
                await _service.Delete(requestId,equipmentId);
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