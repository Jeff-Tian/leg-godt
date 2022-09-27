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
            Environment.SetEnvironmentVariable("ENV", "test");
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
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