using backend.Constants;
using backend.DTOs.Transfer;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route(ApiEndpoints.TransferVouchers)]
    [ApiController]
    public class TransferVoucherController : ControllerBase
    {
        private readonly ITransferService _service;

        public TransferVoucherController(ITransferService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<TransferResponseDTO>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            try
            {
                var result = await _service.GetAll(page, size);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("request")]
        [ProducesResponseType(typeof(TransferResponseDTO), 201)]
        public async Task<ActionResult<TransferResponseDTO>> CreateRequest([FromBody] CreateTransferDTO dto)
        {
            try
            {
                var result = await _service.CreateRequest(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/approve")]
        [ProducesResponseType(typeof(TransferResponseDTO), 200)]
        public async Task<ActionResult<TransferResponseDTO>> ApproveRequest(string id, [FromBody] ApproveTransferDTO dto)
        {
            try
            {
                var result = await _service.ApproveRequest(id, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}