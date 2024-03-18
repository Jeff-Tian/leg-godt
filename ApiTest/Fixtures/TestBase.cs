using Amazon.SimpleEmail;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ApiTest.Fixtures;

public class TestBase
{
    protected HttpClient? _httpClient;

    [TestInitialize]
    public void Setup()
    {
        Environment.SetEnvironmentVariable("ENV", "Test");
        Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", "fake");
        Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", "fake");
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<IAmazonSimpleEmailService>();
                services.AddSingleton<IAmazonSimpleEmailService>(new MockEmailClient());
            });
        });

        _httpClient = application.CreateClient();
    }
}
