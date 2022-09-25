using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Store;
using Web.Models;

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
            
            var scope = application.Services.CreateScope();
            var wecomContext = scope.ServiceProvider.GetService<WecomCorpContext>();
            var fakeCorp = Corporation.GetFakeCorp();

            Debug.Assert(wecomContext != null, nameof(wecomContext) + " != null");

            wecomContext.WecomCorps.Add(fakeCorp);
            wecomContext.SaveChanges();
            
            
            var res = await client.GetAsync("/api/wecom/Token/hardway");
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
    }
}