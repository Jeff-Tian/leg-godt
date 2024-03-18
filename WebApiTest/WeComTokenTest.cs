using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiTest
{
    public class TokenTest
    {
        [Fact]
        public async Task TestGetTokenOk()
        {
            Environment.SetEnvironmentVariable("ENV", "Test");
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
            Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", "fake");
            Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", "fake");
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureAppConfiguration((context, conf) =>
                    {
                        context.HostingEnvironment.EnvironmentName = "Test";
                    });

                    builder.ConfigureTestServices(services =>
                        services.AddSingleton(TestCommon.TestCommon.MockQyapiToken()));
                });

            TestCommon.TestCommon.InjectMockCorpTo(application);

            var client = application.CreateClient();
            var res = await client.GetAsync("/api/wecom/Token/hardway");
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
    }
}
