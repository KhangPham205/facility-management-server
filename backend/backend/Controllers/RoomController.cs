using backend.DTOs.Room.Request;
using backend.DTOs.Room.Response;
using backend.Services.RoomService;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;
        public RoomController(IRoomService service) => _service = service;

        [HttpGet("getAllRooms")]
        public async Task<ActionResult<IEnumerable<RoomResponse>>> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("getRoom")]
        public async Task<ActionResult<RoomResponse>> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("getRoomsByFloor")]
        public async Task<ActionResult<IEnumerable<RoomResponse>>> GetByFloor(string floorId) =>
            Ok(await _service.GetByFloorIdAsync(floorId));

        [HttpPost("createRoom")]
        public async Task<ActionResult<RoomResponse>> Create(RoomCreationRequest request)
        {
            var result = await _service.CreateAsync(request);
            return result == null ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = result.RoomId }, result);
        }

        [HttpPut("updateRoom")]
        public async Task<ActionResult<RoomResponse>> Update(string id, RoomUpdateRequest request)
        {
            var result = await _service.UpdateAsync(id, request);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("deleteRoom")]
        public async Task<IActionResult> Delete(string id) =>
            await _service.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
