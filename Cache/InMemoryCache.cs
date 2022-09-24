namespace Cache;

public class InMemoryCache : CacheInstance
{
    public bool Set(string key, string value)
    {
        return true;
    }
}