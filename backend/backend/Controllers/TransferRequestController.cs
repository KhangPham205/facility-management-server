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
    [Route(ApiEndpoints.TransferRequests)]
    [ApiController]
    public class TransferRequestController : ControllerBase
    {
        private readonly ITransferService _service;

        public TransferRequestController(ITransferService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<TransferRequestResponse>>> GetRequests(
            [FromQuery] EntityFilter<TransferRequest> filter,
            [FromQuery] EntitySort<TransferRequest> orderBy,
            [FromQuery] int page = 1, 
            [FromQuery] int size = 10)
        {
            return Ok(await _service.GetRequests(filter, orderBy, page, size));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransferRequestResponse>> GetById(string id)
        {
            var result = await _service.GetRequestById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TransferRequestResponse>> Create([FromBody] CreateTransferRequestRequest request)
        {
            var result = await _service.CreateRequest(request);
            // Header Location sẽ trỏ về chính controller này
            return CreatedAtAction(nameof(GetById), new { id = result.RequestId }, result);
        }

        [HttpPut("{id}/approve")]
        public async Task<ActionResult<TransferRequestResponse>> Approve(string id, [FromBody] ApproveTransferRequest request)
        {
            var result = await _service.ApproveRequest(id, request);
            return Ok(result);
        }
    }
}