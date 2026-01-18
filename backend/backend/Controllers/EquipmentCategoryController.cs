using backend.Constants;
using backend.DTOs.EquipmentCategory.Request;
using backend.DTOs.EquipmentCategory.Response;
using backend.Models.EquipmentInfo;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.EquipmentCategories)]
    [ApiController]
    public class EquipmentCategoryController : ControllerBase
    {
        private readonly IEquipmentCategoryService _service;

        public EquipmentCategoryController(IEquipmentCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<EquipmentCategoryResponse>>> GetAll(
            [FromQuery] EntityFilter<EquipmentCategory> filter,
            [FromQuery] EntitySort<EquipmentCategory> sort, // Tự động map ?orderBy=Amount-desc
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
        public async Task<ActionResult<EquipmentCategoryResponse>> GetById(string id)
        {
            var result = await _service.GetById(id);
            if (result == null) return NotFound(new { message = "Không tìm thấy loại thiết bị." });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentCategoryResponse>> Create([FromBody] CreateEquipmentCategoryRequest request)
        {
            try
            {
                var result = await _service.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = result.EquipmentCategoryId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EquipmentCategoryResponse>> Update(string id, [FromBody] UpdateEquipmentCategoryRequest request)
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