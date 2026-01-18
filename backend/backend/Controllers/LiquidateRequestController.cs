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
    [Route(ApiEndpoints.LiquidateRequests)]
    [ApiController]
    public class LiquidateRequestController : ControllerBase
    {
        private readonly ILiquidateRequestService _service;

        public LiquidateRequestController(ILiquidateRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<LiquidateRequestResponse>>> GetAll(
            [FromQuery] EntityFilter<LiquidateRequest> filter,
            [FromQuery] EntitySort<LiquidateRequest> sort, // Tự động map ?orderBy=Amount-desc
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            try
            {
                var result = await _service.GetAll(filter, sort, page, size);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LiquidateRequestResponse>> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Thiếu liquidate request id"});

            var result = await _service.GetById(id);
            if (result == null) return NotFound(new { message = "Không tìm thấy phiếu yêu cầu thanh lý" });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<LiquidateRequestResponse>> Create([FromBody] CreateLiquidateRequestRequest request)
        {
            try
            {
                var result = await _service.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = result.RequestId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<LiquidateRequestResponse>> Update(string id, [FromBody] UpdateLiquidateRequestRequest request)
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

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] UpdateLiquidateRequestStatusRequest request)
        {
            try
            {
                await _service.UpdateStatus(id, request);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

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