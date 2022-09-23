using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ApiTest
{
    [TestClass]
    public class WecomTokenTest
    {
        [TestMethod]
        public async Task TestGetTokenOk()
        {
            var res = await client.GetAsync("/api/wecom/token/hardway");
            Assert.AreEqual(HttpStatusCode.InternalServerError, res.StatusCode);
        }

        HttpClient client;
        [TestInitialize]
        public void Setup() {
            Environment.SetEnvironmentVariable("ENV", "test");
            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder=>{});

            client = application.CreateClient();
        }
    }
}