using SubscriptionMaster.Model;
using System.Text.Json;
using System.Text;

namespace SubscriptionMaster.Services
{
    public static class HelperService
    {
        public static async Task<string> CreateCheckoutUrl(string frequency)
        {
            var handler = new HttpClientHandler
            {
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12
            };

            using var client = new HttpClient(handler);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("api-key",
                "796b8b9dbbf46b1d8fd73f68979ae31635da9afabc9dee147adf0440ee7118a8");

            var subscriptionRequestId =
                $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_{Guid.NewGuid()}";

            var request = new SubscriptionRequest
            {
                Amount = "1",
                FirstPaymentIncludedInCycle = "True",
                ServiceId = "100001",
                Currency = "BDT",
                StartDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                ExpiryDate = DateTime.UtcNow.AddMonths(1).ToString("yyyy-MM-dd"),
                Frequency = frequency, //DAILY, WEEKLY or MONTHLY
                SubscriptionType = "BASIC",
                MaxCapRequired = "False",
                MerchantShortCode = "01779707460",
                PayerType = "CUSTOMER",
                PaymentType = "FIXED",
                RedirectUrl = "https://yourdomain.com/success",
                SubscriptionRequestId = subscriptionRequestId,
                SubscriptionReference = "017XXXXXXXX",
                Ckey = "000001"
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = "";
            try
            {
                var response = await client.PostAsync(
                "https://bkashtest.shabox.mobi/home/MultiTournamentInBuildCheckoutUrl",
                content);

                result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

    }
}
