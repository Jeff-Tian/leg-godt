using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Store;
using Web.Models;

namespace ApiTest
{
    [TestClass]
    public class WecomTokenTest
    {
        [TestMethod]
        public async Task TestGetTokenOk()
        {
            var res = await client.GetAsync("/api/wecom/token/hardway");
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        HttpClient client;

        [TestInitialize]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("ENV", "test");
            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
            });

            var scope = application.Services.CreateScope();
            var wecomContext = scope.ServiceProvider.GetService<WecomCorpContext>();
            var fakeCorp = Corporation.GetFakeCorp();

            Debug.Assert(wecomContext != null, nameof(wecomContext) + " != null");

            wecomContext.WecomCorps.Add(fakeCorp);
            wecomContext.SaveChanges();

            client = application.CreateClient();
        }
    }
}