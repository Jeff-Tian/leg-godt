using System.Net;
using Amazon.SimpleEmail;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Moq.Protected;

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

                var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
                mockHttpMessageHandler
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent("Mocked response content")
                    });
                
                services.RemoveAll<HttpClient>();
                var httpClient = new HttpClient(mockHttpMessageHandler.Object);
                
                services.AddSingleton<HttpClient>(httpClient);
            });
        });

        _httpClient = application.CreateClient();
    }
}
