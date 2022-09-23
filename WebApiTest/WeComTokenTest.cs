using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApiTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            Environment.SetEnvironmentVariable("ENV", "test");
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    // ... Configure test services
                });

            var client = application.CreateClient();
            
            
            var res = await client.GetAsync("/api/wecom/Token/hardway");
            Assert.Equal(HttpStatusCode.InternalServerError, res.StatusCode);
        }
    }
}