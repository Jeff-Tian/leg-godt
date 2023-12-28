using System.Net;
using System.Text;
using ApiTest.Fixtures;

namespace ApiTest;

[TestClass]
public class WebhookTest : TestBase
{
    [TestMethod]
    public async Task TestWebhook()
    {
        var response = await _httpClient!.PostAsync("/api/webhook/yuque", new StringContent("{\"data\":{\"title\":\"test\",\"body_html\":\"test\"}}", Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}
