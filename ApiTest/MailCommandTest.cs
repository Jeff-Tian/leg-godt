using System.Net;
using System.Net.Http.Json;
using Amazon.SimpleEmail;
using ApiTest.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.Controllers.Mail;

namespace ApiTest;

[TestClass]
public class MailCommandTest
{
    private HttpClient? _client;

    [TestInitialize]
    public void Setup()
    {
        Environment.SetEnvironmentVariable("ENV", "test");
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<IAmazonSimpleEmailService>();
                services.AddSingleton<IAmazonSimpleEmailService>(new MockEmailClient());
            });
        });

        _client = application.CreateClient();
    }

    [TestMethod]
    public async Task TestSendEmailOk()
    {
        var mailCommand = new MailCommand("jeff.tian@outlook.com", "hello", "world!");
        var response = await _client?.PostAsync("/api/Mail/SendEmail", JsonContent.Create(mailCommand))!;
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadAsStringAsync();
        Assert.AreEqual("Success", body);
    }
}
