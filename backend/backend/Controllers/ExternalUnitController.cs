using backend.Constants;
using backend.Models.Finance;
using backend.Services.Interfaces;
using DTOs.ExternalUnit.Request;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.ExternalUnits)]
    [ApiController]
    public class ExternalUnitController : ControllerBase
    {
        private readonly IExternalUnitService _service;
        public ExternalUnitController(IExternalUnitService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] EntityFilter<ExternalUnit> filter,
            [FromQuery] EntitySort<ExternalUnit> orderBy,
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
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Missing external unit id" });
            var result = await _service.GetById(id);
            if (result == null) return NotFound(new { message = "External unit not found" });
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExternalUnitRequest request)
        {
            try
            {
                var result = await _service.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = result.UnitId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateExternalUnitRequest request)
        {
            try
            {
                var result = await _service.Update(id, request);
                return Ok(result);
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
