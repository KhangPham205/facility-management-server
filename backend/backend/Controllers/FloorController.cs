using backend.DTOs.Floor.Request;
using backend.DTOs.Floor.Response;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorController : ControllerBase
    {
        private readonly IFloorService _floorService;

        public FloorController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        // GET: api/Floor/getAllFloors
        [HttpGet("getAllFloors")]
        public async Task<ActionResult<IEnumerable<FloorResponse>>> GetAllFloors()
        {
            var floors = await _floorService.GetAllFloorsAsync();
            return Ok(floors);
        }

        // GET: api/Floor/getFloor?floorId=guid-string
        [HttpGet("getFloor")]
        public async Task<ActionResult<FloorResponse>> GetFloorById(string floorId)
        {
            if (string.IsNullOrEmpty(floorId))
            {
                return BadRequest("Floor ID is required.");
            }

            var floor = await _floorService.GetFloorByIdAsync(floorId);

            if (floor == null)
            {
                return NotFound($"Floor with ID {floorId} not found.");
            }

            return Ok(floor);
        }

        // GET: api/Floor/getFloorsByBuilding?buildingId=guid-string
        [HttpGet("getFloorsByBuilding")]
        public async Task<ActionResult<IEnumerable<FloorResponse>>> GetFloorsByBuildingId(string buildingId)
        {
            if (string.IsNullOrEmpty(buildingId))
            {
                return BadRequest("Building ID is required.");
            }

            var floors = await _floorService.GetFloorsByBuildingIdAsync(buildingId);
            return Ok(floors);
        }

        // POST: api/Floor/createFloor
        [HttpPost("createFloor")]
        public async Task<ActionResult<FloorResponse>> CreateFloor([FromBody] FloorCreationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newFloorResponse = await _floorService.CreateFloorAsync(request);

            if (newFloorResponse == null)
            {
                return StatusCode(500, "An error occurred while creating the floor.");
            }

            return CreatedAtAction(nameof(GetFloorById), new { floorId = newFloorResponse.FloorId }, newFloorResponse);
        }

        // PUT: api/Floor/updateFloor?floorId=guid-string
        [HttpPut("updateFloor")]
        public async Task<ActionResult<FloorResponse>> UpdateFloor(string floorId, [FromBody] FloorUpdateRequest request)
        {
            if (string.IsNullOrEmpty(floorId))
            {
                return BadRequest("Floor ID is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedFloorResponse = await _floorService.UpdateFloorAsync(floorId, request);

            if (updatedFloorResponse == null)
            {
                return NotFound($"Update failed. Floor with ID {floorId} not found.");
            }

            return Ok(updatedFloorResponse);
        }

        // DELETE: api/Floor/deleteFloor?floorId=guid-string
        [HttpDelete("deleteFloor")]
        public async Task<IActionResult> DeleteFloor(string floorId)
        {
            if (string.IsNullOrEmpty(floorId))
            {
                return BadRequest("Floor ID is required.");
            }

            var isDeleted = await _floorService.DeleteFloorAsync(floorId);

            if (!isDeleted)
            {
                return NotFound($"Delete failed. Floor with ID {floorId} not found or could not be deleted.");
            }

            return NoContent();
        }
    }
}