using backend.DTOs.Statistics;
using backend.Services.Implements;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/v1/statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService _service;

        public StatisticsController(StatisticsService service)
        {
            _service = service;
        }

        [HttpGet("devices")]
        public async Task<ActionResult<DeviceStatisticDTO>> GetDeviceStats()
        {
            var result = await _service.GetDeviceStatistics();
            return Ok(result);
        }

        [HttpGet("borrows")]
        public async Task<ActionResult<List<BorrowTrendDTO>>> GetBorrowStats(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            var result = await _service.GetBorrowStatistics(from, to);
            return Ok(result);
        }

        [HttpGet("costs")]
        public async Task<ActionResult<List<SemesterCostDTO>>> GetCostStats([FromQuery] int year)
        {
            if (year == 0) year = DateTime.Now.Year;
            var result = await _service.GetCostStatisticsBySemester(year);
            return Ok(result);
        }
    }
}
