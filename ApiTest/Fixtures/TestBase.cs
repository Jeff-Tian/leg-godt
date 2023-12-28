using Microsoft.AspNetCore.Mvc.Testing;

namespace ApiTest.Fixtures;

public class TestBase
{
    protected HttpClient? _httpClient;

    [TestInitialize]
    public void Setup()
    {
        Environment.SetEnvironmentVariable("ENV", "test");
        Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", "fake");
        Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", "fake");
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
        });

        _httpClient = application.CreateClient();
    }
}
