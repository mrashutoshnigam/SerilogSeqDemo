using Microsoft.AspNetCore.Mvc;

namespace SerilogSeqDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpGet("info")]
        public IActionResult LogInformation()
        {
            _logger.LogInformation("Processing request to {Endpoint} for user {UserId}", nameof(LogInformation), "User123");
            return Ok("Information log recorded.");
        }

        [HttpGet("warning")]
        public IActionResult LogWarning()
        {
            _logger.LogWarning("Potential issue detected in {Endpoint}. Check configuration for {Setting}", nameof(LogWarning), "ApiKey");
            return Ok("Warning log recorded.");
        }

        [HttpGet("error")]
        public IActionResult LogError()
        {
            try
            {
                throw new InvalidOperationException("Simulated error in the system.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in {Endpoint} while processing request.", nameof(LogError));
                return StatusCode(500, "Error log recorded.");
            }
        }

        [HttpGet("enrichederror")]
        public IActionResult LogErrorWithEnrichers()
        {
            try
            {
                throw new InvalidOperationException("Simulated error in the system.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in {Endpoint} for user {UserId} with request {RequestId}", nameof(LogError), "User123", Request.HttpContext.TraceIdentifier);
                return StatusCode(500, "Error log recorded.");
            }
        }
    }
}
