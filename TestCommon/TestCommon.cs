using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using Store;
using Web.Models;

namespace TestCommon;

public class TestCommon
{
    public static HttpClient MockQyapiToken()
    {
        var mockCorp = Corporation.GetFakeCorp();
        
        var msgHandler = new MockHttpMessageHandler();
        
        msgHandler.When(
                $"https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={mockCorp.CorpId}&corpsecret={mockCorp.CorpSecret}")
            .Respond("application/json", @"{
    ""errcode"": 0,
        ""errmsg"": ""ok"",
        ""access_token"": ""abc"",
        ""expires_in"": 7200
    }");

        return new HttpClient(msgHandler);
    }

    public static void InjectMockCorpTo(WebApplicationFactory<Program> application)
    {
        var scope = application.Services.CreateScope();
        var wecomContext = scope.ServiceProvider.GetService<WecomCorpContext>();
        var fakeCorp = Corporation.GetFakeCorp();

        Debug.Assert(wecomContext != null, nameof(wecomContext) + " != null");

        wecomContext.WecomCorps.Add(fakeCorp);
        wecomContext.SaveChanges();
    }
}