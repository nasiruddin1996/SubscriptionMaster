using Microsoft.AspNetCore.Mvc;
using SubscriptionMaster.Model;
using SubscriptionMaster.Services;
using System.Text;
using System.Text.Json;

namespace SubscriptionMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController : ControllerBase
    {
        [HttpGet("generate")]
        public IActionResult Generate()
        {
            var random = new Random();

            var request = new SubscriptionRequest
            {
                Amount = "1",
                FirstPaymentIncludedInCycle = "True",
                ServiceId = "100001",
                Currency = "BDT",
                StartDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                ExpiryDate = DateTime.UtcNow.AddMonths(1).ToString("yyyy-MM-dd"),
                Frequency = "WEEKLY",
                SubscriptionType = "BASIC",
                MaxCapRequired = "False",
                MerchantShortCode = "01779707460",
                PayerType = "CUSTOMER",
                PaymentType = "FIXED",
                RedirectUrl = "https://google.com",
                SubscriptionRequestId = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_{Guid.NewGuid()}",
                SubscriptionReference = "017XXXXXXXX",
                Ckey = "000001"
            };

            var json = JsonSerializer.Serialize(request);
            return Ok(json);
        }

        [HttpGet("checkout/{frequency}")]
        public async Task<IActionResult> Checkout(string frequency)
        {
            var url = await HelperService.CreateCheckoutUrl(frequency);
            return Ok(url);
        }
    }
}
