using backend.Constants;
using backend.DTOs.Import.Request;
using backend.DTOs.Import.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models.Import;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.ImportRequestDetails)]
    [ApiController]
    public class ImportRequestDetailController : ControllerBase
    {
        private readonly IImportRequestDetailService _service;

        public ImportRequestDetailController(IImportRequestDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<ImportRequestDetailResponse>>> GetAll(
            [FromQuery] EntityFilter<ImportRequestDetail> filter,
            [FromQuery] EntitySort<ImportRequestDetail> orderBy,
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
        public async Task<ActionResult<ImportRequestDetailResponse>> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Thiếu equipment id"});

            var result = await _service.GetById(id);
            if (result == null) return NotFound(new { message = "Không tìm thấy thiết bị" });
            return Ok(result);
        }

        //[HttpPost]
        //public async Task<ActionResult<ImportRequestDetailResponse>> Create([FromBody] CreateImportRequestDetailRequest request)
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
        //public async Task<ActionResult<ImportRequestDetailResponse>> Update(string id, [FromBody] UpdateImportRequestDetailRequest request)
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
        //public async Task<IActionResult> UpdateStatus(string id, [FromBody] UpdateImportRequestDetailStatusRequest request)
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