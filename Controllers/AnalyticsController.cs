using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiffinManagement.Services;

namespace TiffinManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly ILogger<AnalyticsController> _logger;
        public AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger)
        {
            _analyticsService = analyticsService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAnalytics([FromQuery] string timeframe = "monthly")
        {
            DateTime toDate = DateTime.UtcNow;
            DateTime fromDate;

            switch (timeframe.ToLower())
            {
                case "weekly":
                    fromDate = toDate.AddDays(-7);
                    break;
                case "monthly":
                    fromDate = new DateTime(toDate.Year, toDate.Month, 1);
                    break;
                case "yearly":
                    fromDate = new DateTime(toDate.Year, 1, 1);
                    break;
                case "all":
                    fromDate = DateTime.MinValue; // Include all orders
                    break;
                default:
                    return BadRequest("Invalid timeframe. Use 'weekly', 'monthly', 'yearly', or 'all'.");
            }

            try
            {
                var analytics = await _analyticsService.GetAnalyticsAsync(fromDate, toDate);
                return Ok(analytics);
            }
            catch (NullReferenceException ex)
            {
                // Log the exception (you could use a logger here, like Serilog)
                _logger.LogError($"Null reference exception: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
            catch (Exception ex)
            {
                // Handle other types of exceptions here
                _logger.LogError($"Exception: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }


    }
}
