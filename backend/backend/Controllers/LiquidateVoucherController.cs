using backend.Constants;
using backend.DTOs.Liquidate.Request;
using backend.DTOs.Liquidate.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models;
using backend.Models.Liquidate;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.LiquidateVouchers)]
    [ApiController]
    public class LiquidateVoucherController : ControllerBase
    {
        private readonly ILiquidateVoucherService _service;

        public LiquidateVoucherController(ILiquidateVoucherService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<LiquidateVoucherResponse>>> GetAll(
            [FromQuery] EntityFilter<LiquidateVoucher> filter,
            [FromQuery] EntitySort<LiquidateVoucher> sort, // Tự động map ?orderBy=Amount-desc
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
        public async Task<ActionResult<LiquidateVoucherResponse>> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Thiếu liquidate voucher id"});

            var result = await _service.GetById(id);
            if (result == null) return NotFound(new { message = "Không tìm thấy phiếu thanh lý" });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<LiquidateVoucherResponse>> Create([FromBody] CreateLiquidateVoucherRequest request)
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
        //public async Task<ActionResult<LiquidateVoucherResponse>> Update(string id, [FromBody] UpdateLiquidateVoucherRequest request)
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