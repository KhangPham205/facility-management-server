using backend.DTOs.Toa.Request;
using backend.DTOs.Toa.Response;
using backend.Services.ToaService;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToaController : ControllerBase
    {
        private readonly IToaService _toaService;

        public ToaController(IToaService toaService)
        {
            _toaService = toaService;
        }

        // GET: api/Toas
        [HttpGet("getAllToas")]
        public async Task<ActionResult<IEnumerable<ToaResponse>>> GetAllToas()
        {
            var toas = await _toaService.GetAllToasAsync();
            return Ok(toas);
        }

        // GET: api/Toas/maToa001
        [HttpGet("getToa")]
        public async Task<ActionResult<ToaResponse>> GetToa(string maToa)
        {
            var toa = await _toaService.GetToaByIdAsync(maToa);

            if (toa == null)
            {
                return NotFound();
            }

            return Ok(toa);
        }

        // POST: api/Toas
        // Nhận ToaCreationRequest DTO
        [HttpPost("createToa")]
        public async Task<ActionResult<ToaResponse>> CreateToa(ToaCreationRequest request)
        {
            // 1. Validation tự động được thực hiện nhờ [ApiController] và Data Annotations trong DTO Request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // 2. Gọi Service để xử lý logic và tạo mới
            var newToaResponse = await _toaService.CreateToaAsync(request);

            // 3. Trả về kết quả (DTO Response)
            return CreatedAtAction(nameof(GetToa), new { maToa = newToaResponse.maToa }, newToaResponse);
        }

        [HttpPut("updateToa")]
        public async Task<ActionResult<ToaResponse>> UpdateToa(string maToa, ToaUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateToaResponse = await _toaService.UpdateToaAsync(maToa, request);

            if (updateToaResponse == null)
            {
                return NotFound();
            }

            return Ok(updateToaResponse);
        }

        [HttpDelete("deleteToa")]
        public async Task<IActionResult> DeleteToa(string maToa)
        {
            var isDeleted = await _toaService.DeleteToaAsync(maToa);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
