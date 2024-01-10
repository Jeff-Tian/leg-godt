using System.Net;
using System.Text;
using ApiTest.Fixtures;
using FluentAssertions;

namespace ApiTest;

[TestClass]
public class WebhookTest : TestBase
{
    [TestMethod]
    public async Task TestWebhook()
    {
        var response = await _httpClient!.PostAsync("/api/webhook/yuque", new StringContent("{\"data\":{\"title\":\"test\",\"body_html\":\"test_body_html\"}}", Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        response.Content.Headers.ContentType?.MediaType.Should().Be("text/plain");
        var body = await response.Content.ReadAsStringAsync();
        body.Should().Be("test_body_html");
    }
}
