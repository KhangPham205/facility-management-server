
using backend.Constants;
using backend.DTOs.Equipment;
using backend.DTOs.Equipment.Request;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route(ApiEndpoints.Equipments)]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _service;

        public EquipmentController(IEquipmentService service)
        {
            _service = service;
        }

        // --- Category API ---
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _service.GetAllCategories();
            return Ok(result);
        }

        [HttpPost("categories")]
        // [Authorize(Roles = "FacilityManager")] // Chỉ quản lý mới được thêm
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO dto)
        {
            try
            {
                var result = await _service.CreateCategory(dto);
                return CreatedAtAction(nameof(GetCategories), new { id = result.CategoryId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            try
            {
                await _service.DeleteCategory(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // --- Equipment API ---
        [HttpGet]
        public async Task<IActionResult> GetAllEquipments()
        {
            var result = await _service.GetAllEquipments();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEquipment(string id)
        {
            var result = await _service.GetEquipmentById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEquipment([FromBody] CreateEquipmentDTO dto)
        {
            try
            {
                var result = await _service.CreateEquipment(dto);
                return CreatedAtAction(nameof(GetEquipment), new { id = result.EquipmentId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] UpdateEquipmentStatusDTO dto)
        {
            try
            {
                var result = await _service.UpdateEquipmentStatus(id, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}