using backend.Constants;
using backend.DTOs.Borrow;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route(ApiEndpoints.BorrowVouchers)]
    [ApiController]
    public class BorrowVoucherController : ControllerBase
    {
        private readonly IBorrowService _borrowService;

        public BorrowVoucherController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }

        // POST: api/BorrowVoucher/request
        [HttpPost("request")]
        public async Task<IActionResult> CreateRequest([FromBody] BorrowRequestDTO dto)
        {
            try
            {
                var result = await _borrowService.CreateRequest(dto);
                return Ok(new { message = "Request created successfully", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/BorrowVoucher/{id}/approve
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveRequest(string id, [FromBody] ApproveBorrowDTO dto)
        {
            try
            {
                var result = await _borrowService.ApproveRequest(id, dto);
                return Ok(new { message = "Request processed", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/BorrowVoucher/{id}/return
        [HttpPost("{id}/return")]
        public async Task<IActionResult> ReturnDevice(string id)
        {
            try
            {
                var result = await _borrowService.ReturnDevice(id);
                return Ok(new { message = "The device has been returned successfully.\r\n", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/BorrowVoucher
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _borrowService.GetHistory());
        }
    }
}