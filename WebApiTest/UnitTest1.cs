using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApiTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    // ... Configure test services
                });

            var client = application.CreateClient();
            
            
            var res = await client.GetAsync("/api/wecom/token/hardway");
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);

            var body = await res.Content.ReadAsStringAsync();
            Assert.Equal("", body);
        }
    }
}