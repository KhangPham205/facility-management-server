using backend.Constants;
using backend.DTOs.Booking.Request;
using backend.DTOs.Booking.Response;
using backend.Models.Area;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.RoomBookings)]
    [ApiController]
    public class RoomBookingController : ControllerBase
    {
        private readonly IRoomBookingService _service;

        public RoomBookingController(IRoomBookingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<RoomBookingResponse>>> GetAll(
            [FromQuery] EntityFilter<RoomBooking> filter,
            [FromQuery] EntitySort<RoomBooking> orderBy,
            [FromQuery] int page = 1, 
            [FromQuery] int size = 10)
        {
            return Ok(await _service.GetAll(filter, orderBy, page, size));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<RoomBookingResponse>> GetById(string id)
        {
            var result = await _service.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RoomBookingResponse>> Create([FromBody] CreateBookingRequest request)
        {
            var result = await _service.Create(request);
            return Ok(result);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<RoomBookingResponse>> UpdateStatus(string id, [FromBody] UpdateBookingStatusRequest request)
        {
            try
            {
                var result = await _service.UpdateStatus(id, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}/approve")]
        // [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<RoomBookingResponse>> Approve(string id, [FromBody] ApproveBookingRequest request)
        {
            var result = await _service.Approve(id, request);
            return Ok(result);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(string id)
        {
            await _service.Cancel(id);
            return NoContent();
        }
    }
}