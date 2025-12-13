using backend.DTOs.Tang.Request;
using backend.DTOs.Tang.Response;
using backend.Services.TangService;
using backend.Services.ToaService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TangController : ControllerBase
    {
        private readonly ITangService _tangService;

        public TangController(ITangService tangService)
        {
            _tangService = tangService;
        }

        [HttpGet("getTang")]
        public async Task<ActionResult<TangResponse>> GetTangById(string maTang)
        {
            var tang = await _tangService.GetTangByIdAsync(maTang);

            if (tang == null)
            {
                return NotFound();
            }

            return Ok(tang);
        }

        [HttpGet("getAllTang")]
        public async Task<ActionResult<IEnumerable<TangResponse>>> GetAllTang()
        {
            var tangs = await _tangService.GetAllTangAsync();

            return Ok(tangs);
        }

        [HttpGet("getAllTangOfToa")]
        public async Task<ActionResult<IEnumerable<TangResponse>>> GetAllTangOfToa(string maToa)
        {
            var tangs = await _tangService.GetAllTangOfToaAsync(maToa);

            return Ok(tangs);
        }

        [HttpPost("createTang")]
        public async Task<ActionResult<TangResponse>> CreateTang(TangCreationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TangResponse? newTang = await _tangService.CreateTangAsync(request);

            if (newTang == null)
                return NotFound();

            return newTang;
        }

        [HttpPut("updateTang")]
        public async Task<ActionResult<TangResponse>> UpdateTang(string maTang, TangUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TangResponse? updatedTang = await _tangService.UpdateTangAsync(maTang, request);

            if(updatedTang == null)
                return NotFound();

            return updatedTang;
        }

        [HttpDelete("deleteTang")]
        public async Task<IActionResult> DeleteTang(string maTang)
        {
            bool isDeleted = await _tangService.DeleteTangAsync(maTang);

            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
    }
}
