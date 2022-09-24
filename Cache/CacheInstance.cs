namespace Cache;

public interface CacheInstance
{
    public bool Set(string key, string value);
}