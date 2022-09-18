using FluentAssertions.Execution;
using Xunit;

namespace SpecFlowLegGodt;

[Binding]
public class WecomStepDefinitions
{
    private string _enterprise;
    
    [Given(@"the enterprise is ""(.*)""")]
    public void GivenTheEnterpriseIs(string hardway)
    {
        _enterprise = hardway;
    }

    [Then(@"the current token is ""(.*)""")]
    public void ThenTheCurrentTokenIs(string abc)
    {
        var actualToken = Wecom.Wecom.GetAccessToken(_enterprise);
        Assert.Equal(abc, actualToken);
    }
}