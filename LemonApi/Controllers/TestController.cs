using Microsoft.AspNetCore.Mvc;

namespace LemonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Get")]
        public int Get(int value)
        {
            return value * value;
        }
    }
}