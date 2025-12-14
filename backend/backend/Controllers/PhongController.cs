using backend.DTOs.Phong.Request;
using backend.DTOs.Phong.Response;
using backend.Services.PhongService;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongController : ControllerBase
    {
        private readonly IPhongService _phongService;

        public PhongController(IPhongService phongService)
        {
            _phongService = phongService;
        }

        [HttpGet("getPhong")]
        public async Task<ActionResult<PhongResponse?>> GetPhongByIdAsync(string maPhong)
        {
            if (string.IsNullOrWhiteSpace(maPhong))
                return BadRequest("Mã phòng không được để trống");

            PhongResponse response = await _phongService.GetPhongByIdAsync(maPhong);

            if (response == null)
                return NotFound("Không tìm thấy phòng.");

            return Ok(response);
        }

        [HttpGet("getAllPhong")]
        public async Task<ActionResult<IEnumerable<PhongResponse>>> GetAllPhongAsync()
        {
            IEnumerable<PhongResponse> phongs = await _phongService.GetAllPhongAsync();

            return Ok(phongs);
        }

        [HttpGet("getAllPhongOfToa")]
        public async Task<ActionResult<IEnumerable<PhongResponse>>> GetAllPhongOfToaAsync(string maToa)
        {
            IEnumerable<PhongResponse> phongs = await _phongService.GetAllPhongOfToaAsync(maToa);

            return Ok(phongs);
        }

        [HttpGet("getAllPhongOfTang")]
        public async Task<ActionResult<IEnumerable<PhongResponse>>> GetAllPhongOfTangAsync(string maTang)
        {
            IEnumerable<PhongResponse> phongs = await _phongService.GetAllPhongOfTangAsync(maTang);

            return Ok(phongs);
        }

        [HttpPost("createPhong")]
        public async Task<ActionResult<PhongResponse?>> CreatePhongAsync(PhongCreationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _phongService.CreatePhongAsync(request);

            if (response == null)
                return NotFound("Thêm phòng không thành công");

            return CreatedAtAction(nameof(GetPhongByIdAsync), new { maPhong = response.maPhong }, response);
        }

        [HttpPut("updatePhong")]
        public async Task<ActionResult<PhongResponse?>> UpdatePhongAsync(string maPhong, PhongUpdateRequest request)
        {
            if (maPhong == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _phongService.UpdatePhongAsync(maPhong, request);

            if (response == null)
                return NotFound("Sửa thông tin phòng không thành công.");

            return Ok(response);
        }

        [HttpDelete("deletePhong")]
        public async Task<IActionResult> DeletePhongAsync(string maPhong)
        {
            if (String.IsNullOrWhiteSpace(maPhong))
                return BadRequest("Mã phòng không được để trống");

            bool isSuccessed = false;
            isSuccessed = await _phongService.DeletePhongAsync(maPhong);

            if (!isSuccessed)
                return NotFound("Xóa phòng không thành công.");

            return NoContent();
        }
    }
}
