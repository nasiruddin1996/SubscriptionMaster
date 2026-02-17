using Microsoft.AspNetCore.Mvc;

namespace SubscriptionMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniqueController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUniqueString()
        {
            var uniqueString = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_{Guid.NewGuid()}";
            return Ok(uniqueString);
        }
    }
}
