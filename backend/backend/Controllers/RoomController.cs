using backend.Constants;
using backend.DTOs.Room.Request;
using backend.DTOs.Room.Response;
using backend.Enums;
using backend.Models.Area;
using backend.Services.Implements;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.Rooms)]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomController(IRoomService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<RoomResponse>>> GetAll(
            [FromQuery] EntityFilter<Room> filter,
            [FromQuery] EntitySort<Room> orderBy,
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
        public async Task<ActionResult<RoomResponse>> GetById(string id)
        {
            var result = await _service.GetById(id);
            if (result == null) return NotFound(new { message = "Không tìm thấy phòng." });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RoomResponse>> Create([FromBody] CreateRoomRequest request)
        {
            try
            {
                var result = await _service.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = result.RoomId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoomResponse>> Update(string id, [FromBody] UpdateRoomRequest request)
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

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] UpdateRoomStatusRequest request)
        {
            var result = await _service.UpdateRoomStatusAsync(id, request);

            if (!result)
            {
                return NotFound(new { message = "Không tìm thấy phòng hoặc cập nhật thất bại." });
            }

            return Ok(new { message = "Cập nhật trạng thái thành công." });
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