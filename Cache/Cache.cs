using StackExchange.Redis;

namespace Cache;

public static class Cache
{
    public static bool Set(string key, string value)
    {
        CacheInstance instance = Environment.GetEnvironmentVariable("ENV") is "Test"
            ? new InMemoryCache()
            : new RedisCache();
        
        return instance.Set(key, value);
    }
}
