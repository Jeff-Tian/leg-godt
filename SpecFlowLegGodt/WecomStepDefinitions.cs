using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using RichardSzalay.MockHttp;
using Store;
using UniHeart.Wecom;
using Web.Models;
using Xunit;

namespace SpecFlowLegGodt;

[Binding]
public class WecomStepDefinitions
{
    private string _enterprise;
    private Wecom _wecom;

    [Given(@"the enterprise is ""(.*)""")]
    public void GivenTheEnterpriseIs(string hardway)
    {
        _enterprise = hardway;

        var _mockCorp = new Corporation { CorpId = "1234", CorpSecret = "abcd", Name = hardway };

        var _msgHandler = new MockHttpMessageHandler();
        _msgHandler.When(
                $"https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={_mockCorp.CorpId}&corpsecret={_mockCorp.CorpSecret}")
            .Respond("application/json", @"{
    ""errcode"": 0,
        ""errmsg"": ""ok"",
        ""access_token"": ""abc"",
        ""expires_in"": 7200
    }");

        var options = new DbContextOptionsBuilder<WecomCorpContext>().UseInMemoryDatabase(databaseName: "WecomCorps")
            .Options;

        var context = new WecomCorpContext(options);

        context.WecomCorps.Add(_mockCorp);
        context.SaveChanges();

        _wecom = new Wecom(context, new HttpClient(_msgHandler));
    }

    [Then(@"the current token is ""(.*)""")]
    public async void ThenTheCurrentTokenIs(string abc)
    {
        var actualToken = await _wecom.GetAccessToken(_enterprise);
        Assert.Equal(new AccessToken() { access_token = "abc", errcode = 0, errmsg = "ok", expires_in = 7200 }.access_token,
            actualToken.access_token);
    }
}