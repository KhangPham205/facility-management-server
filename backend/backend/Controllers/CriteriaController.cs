using backend.Constants;
using backend.DTOs.Criteria.Request;
using backend.DTOs.Criteria.Response;
using backend.Models.EquipmentInfo;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.Criterias)]
    [ApiController]
    public class CriteriaController : ControllerBase
    {
        private readonly ICriteriaService _service;

        public CriteriaController(ICriteriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<CriteriaResponse>>> GetAll(
            [FromQuery] EntityFilter<Criteria> filter,
            [FromQuery] EntitySort<Criteria> sort, // Tự động map ?orderBy=Amount-desc
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
        public async Task<ActionResult<CriteriaResponse>> GetById(string id)
        {
            var result = await _service.GetById(id);
            if (result == null) return NotFound(new { message = "Không tìm thấy tiêu chí." });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CriteriaResponse>> Create([FromBody] CreateCriteriaRequest request)
        {
            try
            {
                var result = await _service.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = result.CriteriaId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("list")]
        public async Task<ActionResult<List<CriteriaResponse>>> CreateList([FromBody] CreateCriteriaListRequest request)
        {
            try
            {
                var results = await _service.CreateList(request);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CriteriaResponse>> Update(string id, [FromBody] UpdateCriteriaRequest request)
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