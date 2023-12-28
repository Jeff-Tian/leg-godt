using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTest
{
    [TestClass]
    public class WecomTokenTest
    {
        HttpClient? client;

        [TestMethod]
        public async Task TestGetTokenOk()
        {
            var res = await client?.GetAsync("/api/wecom/token/hardway")!;
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
            var body = await res.Content.ReadAsStringAsync();
            Assert.AreEqual("{\"errcode\":0,\"errmsg\":\"ok\",\"access_token\":\"abc\",\"expires_in\":7200}", body);
        }


        [TestInitialize]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("ENV", "test");
            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                    services.AddSingleton(TestCommon.TestCommon.MockQyapiToken()));
            });

            TestCommon.TestCommon.InjectMockCorpTo(application);

            client = application.CreateClient();
        }
    }
}
