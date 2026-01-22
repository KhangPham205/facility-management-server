using backend.Constants;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route(ApiEndpoints.Invoice)]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _service;
        public InvoiceController(IInvoiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] Plainquire.Filter.EntityFilter<backend.Models.Finance.Invoice> filter,
            [FromQuery] Plainquire.Sort.EntitySort<backend.Models.Finance.Invoice> orderBy,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            try
            {
                var result = await _service.GetAll(filter, orderBy, page, size);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Missing invoice id" });
            var result = await _service.GetById(id);
            if (result == null) return NotFound(new { message = "Invoice not found" });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] backend.DTOs.Invoice.Request.CreateInvoiceRequest request)
        {
            try
            {
                var result = await _service.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = result.InvoiceId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
