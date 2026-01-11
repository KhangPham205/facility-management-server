using backend.DTOs.Area.RoomType.Request;
using backend.DTOs.Area.RoomType.Response;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _service;

        public RoomTypeController(IRoomTypeService service)
        {
            _service = service;
        }

        [HttpGet("getAllRoomTypes")]
        public async Task<ActionResult<IEnumerable<RoomTypeResponse>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("getRoomType")]
        public async Task<ActionResult<RoomTypeResponse>> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound($"RoomType ID {id} not found.");
            return Ok(result);
        }

        [HttpPost("createRoomType")]
        public async Task<ActionResult<RoomTypeResponse>> Create(RoomTypeCreationRequest request)
        {
            var result = await _service.CreateAsync(request);
            if (result == null) return BadRequest("Could not create RoomType.");

            return CreatedAtAction(nameof(GetById), new { id = result.RoomTypeId }, result);
        }

        [HttpPut("updateRoomType")]
        public async Task<ActionResult<RoomTypeResponse>> Update(string id, RoomTypeUpdateRequest request)
        {
            var result = await _service.UpdateAsync(id, request);
            if (result == null) return NotFound($"Update failed. ID {id} not found.");
            return Ok(result);
        }

        [HttpDelete("deleteRoomType")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound($"Delete failed. ID {id} not found.");
            return NoContent();
        }
    }
}