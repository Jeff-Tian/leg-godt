using Xunit;

namespace SpecFlowLegGodt.Steps;

[Binding]
public class CacheStepDefinitions
{
    private string _key;
    private string _value;
    private string _res;
    
    [Given(@"the value is '(.*)'")]
    public void GivenTheValueIs(string value)
    {
        _value = value;
    }

    [Given(@"the key is '(.*)'")]
    public void GivenTheKeyIs(string key)
    {
        _key = key;
    }

    [When(@"set cache")]
    public void WhenSetCache()
    {
        _res = Cache.Cache.Set(_key, _value);
    }

    [Then(@"the result should be success")]
    public void ThenTheResultShouldBeSuccess()
    {
        Assert.Equal("success", _res);
    }
}