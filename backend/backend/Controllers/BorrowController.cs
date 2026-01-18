using backend.Constants;
using backend.DTOs.Borrow.Request;
using backend.DTOs.Borrow.Response;
using backend.Models.Borrow;
using backend.Services.Interfaces;
using backend.Utils;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers
{
    [Route(ApiEndpoints.BorrowVouchers)]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowService _service;
        private readonly JwtUtils _jwtUtils;

        public BorrowController(IBorrowService service, JwtUtils jwtUtils)
        {
            _service = service;
            _jwtUtils = jwtUtils;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<BorrowVoucherResponse>>> GetAll(
            [FromQuery] EntityFilter<BorrowVoucher> filter,
            [FromQuery] EntitySort<BorrowVoucher> sort,
            [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            return Ok(await _service.GetAll(filter, sort, page, size));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowVoucherResponse>> GetById(string id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<BorrowVoucherResponse>> Create([FromBody] CreateBorrowRequest request)
        {
            var userId = _jwtUtils.GetCurrentUserId();

            var result = await _service.Create(userId, request);
            return CreatedAtAction(nameof(GetById), new { id = result.BorrowId }, result);
        }

        [HttpPut("{id}/approve")]
        public async Task<ActionResult<BorrowVoucherResponse>> Approve(string id, [FromBody] ApproveBorrowRequest request)
        {
            var result = await _service.Approve(id, request);
            return Ok(result);
        }

        [HttpPut("{id}/return")]
        public async Task<ActionResult<BorrowVoucherResponse>> Return(string id)
        {
            var result = await _service.Return(id);
            return Ok(result);
        }
    }
}