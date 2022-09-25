using RichardSzalay.MockHttp;
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
}