using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ApiTest;

[TestClass]
public class MailCommandTest
{
    HttpClient? client;

    [TestInitialize]
    public void Setup()
    {
        Environment.SetEnvironmentVariable("ENV", "test");
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
        });

        client = application.CreateClient();
    }

    [TestMethod]
    public async Task TestSendEmailOk()
    {
        var response = await client?.PostAsync("/api/Mail/SendEmail", null)!;
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadAsStringAsync();
        Assert.AreEqual("Success", body);
    }
}
