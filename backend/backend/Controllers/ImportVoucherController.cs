using backend.Constants;
using backend.DTOs.Import.Request;
using backend.DTOs.Import.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models;
using backend.Models.Import;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.ImportVouchers)]
    [ApiController]
    public class ImportVoucherController : ControllerBase
    {
        private readonly IImportVoucherService _service;

        public ImportVoucherController(IImportVoucherService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<ImportVoucherResponse>>> GetAll(
            [FromQuery] EntityFilter<ImportVoucher> filter,
            [FromQuery] EntitySort<ImportVoucher> orderBy,
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
        public async Task<ActionResult<ImportVoucherResponse>> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Thiếu import voucher id"});

            var result = await _service.GetById(id);
            if (result == null) return NotFound(new { message = "Không tìm thấy phiếu nhập" });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ImportVoucherResponse>> Create([FromBody] CreateImportVoucherRequest request)
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
        //public async Task<ActionResult<ImportVoucherResponse>> Update(string id, [FromBody] UpdateImportVoucherRequest request)
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