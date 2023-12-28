using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Web.Controllers.Mail;

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
        var mailCommand = new MailCommand("jeff.tian@outlook.com", "hello", "world!");
        var response = await client?.PostAsync("/api/Mail/SendEmail", JsonContent.Create(mailCommand))!;
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadAsStringAsync();
        Assert.AreEqual("Success", body);
    }
}
