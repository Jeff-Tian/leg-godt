using Microsoft.EntityFrameworkCore;
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

        var mockCorp = Corporation.GetFakeCorp();
        mockCorp.Name = hardway;

        var httpClient = TestCommon.TestCommon.MockQyapiToken();

        var options = new DbContextOptionsBuilder<WecomCorpContext>().UseInMemoryDatabase(databaseName: "WecomCorps")
            .Options;

        var context = new WecomCorpContext(options);

        context.WecomCorps.Add(mockCorp);
        context.SaveChanges();

        _wecom = new Wecom(context, httpClient);
    }

    [Then(@"the current token is ""(.*)""")]
    public async void ThenTheCurrentTokenIs(string abc)
    {
        var actualToken = await _wecom.GetAccessToken(_enterprise);
        Assert.Equal(new AccessToken() { access_token = "abc", errcode = 0, errmsg = "ok", expires_in = 7200 }.access_token,
            actualToken.access_token);
    }
}