using backend.DTOs.Building.Request;
using backend.DTOs.Building.Response;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        // GET: api/Building/getAllBuildings
        [HttpGet("getAllBuildings")]
        public async Task<ActionResult<IEnumerable<BuildingResponse>>> GetAllBuildings()
        {
            var buildings = await _buildingService.GetAllBuildingsAsync();
            return Ok(buildings);
        }

        // GET: api/Building/getBuilding?id=guid-string
        [HttpGet("getBuilding")]
        public async Task<ActionResult<BuildingResponse>> GetBuilding(string id)
        {
            var building = await _buildingService.GetBuildingByIdAsync(id);

            if (building == null)
            {
                // Thông báo lỗi rõ ràng hơn thay vì chỉ "Mã phòng"
                return NotFound($"Không tìm thấy tòa nhà với mã: {id}");
            }

            return Ok(building);
        }

        // POST: api/Building/createBuilding
        [HttpPost("createBuilding")]
        public async Task<ActionResult<BuildingResponse>> CreateBuilding(BuildingCreationRequest request)
        {
            // Validation tự động được thực hiện nhờ [ApiController]
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newBuildingResponse = await _buildingService.CreateBuildingAsync(request);

            // Trả về 201 Created cùng với đường dẫn lấy tài nguyên vừa tạo
            // Lưu ý: "BuildingId" phải khớp với tên thuộc tính trong BuildingResponse
            return CreatedAtAction(nameof(GetBuilding), new { id = newBuildingResponse.BuildingId }, newBuildingResponse);
        }

        // PUT: api/Building/updateBuilding?id=guid-string
        [HttpPut("updateBuilding")]
        public async Task<ActionResult<BuildingResponse>> UpdateBuilding(string id, BuildingUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedBuildingResponse = await _buildingService.UpdateBuildingAsync(id, request);

            if (updatedBuildingResponse == null)
            {
                return NotFound($"Cập nhật thất bại. Không tìm thấy tòa nhà với mã: {id}");
            }

            return Ok(updatedBuildingResponse);
        }

        // DELETE: api/Building/deleteBuilding?id=guid-string
        [HttpDelete("deleteBuilding")]
        public async Task<IActionResult> DeleteBuilding(string id)
        {
            var isDeleted = await _buildingService.DeleteBuildingAsync(id);

            if (!isDeleted)
            {
                return NotFound($"Xóa tòa nhà thất bại hoặc tòa nhà mã {id} không tồn tại.");
            }

            return NoContent(); // Trả về 204 No Content khi xóa thành công
        }
    }
}