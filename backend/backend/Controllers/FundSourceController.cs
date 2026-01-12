using backend.Constants;
using backend.DTOs.FundSource.Request;
using backend.DTOs.FundSource.Response;
using backend.Models.Finance;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.FundSources)]
    [ApiController]
    public class FundSourceController : ControllerBase
    {
        private readonly IFundSourceService _service;

        public FundSourceController(IFundSourceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<FundSourceResponse>>> GetAll(
            [FromQuery] EntityFilter<FundSource> filter,
            [FromQuery] EntitySort<FundSource> sort, // Tự động map ?orderBy=Amount-desc
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
        public async Task<ActionResult<FundSourceResponse>> GetById(string id)
        {
            var result = await _service.GetById(id);
            if (result == null) return NotFound(new { message = "Không tìm thấy nguồn kinh phí." });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<FundSourceResponse>> Create([FromBody] CreateFundSourceRequest request)
        {
            try
            {
                var result = await _service.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = result.SourceId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FundSourceResponse>> Update(string id, [FromBody] UpdateFundSourceRequest request)
        {
            try
            {
                var result = await _service.Update(id, request);
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