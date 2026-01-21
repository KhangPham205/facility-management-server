using backend.Constants;
using backend.DTOs.Transfer.Request;
using backend.DTOs.Transfer.Response;
using backend.Models.Transfer;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Controllers.Transfer
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
        public async Task<ActionResult<PageVO<TransferVoucherResponse>>> GetVouchers(
            [FromQuery] EntityFilter<TransferVoucher> filter,
            [FromQuery] EntitySort<TransferVoucher> sort,
            [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            return Ok(await _service.GetVouchers(filter, sort, page, size));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransferVoucherResponse>> GetById(string id)
        {
            var result = await _service.GetVoucherById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TransferVoucherResponse>> Create([FromBody] CreateTransferVoucherRequest request)
        {
            var result = await _service.CreateVoucher(request);
            return CreatedAtAction(nameof(GetById), new { id = result.TransferId }, result);
        }
    }
}