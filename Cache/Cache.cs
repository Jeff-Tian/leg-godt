using StackExchange.Redis;

namespace Cache;

public static class Cache
{
    public static bool Set(string key, string value)
    {
        CacheInstance instance = Environment.GetEnvironmentVariable("ENV") is "test"
            ? new InMemoryCache()
            : new RedisCache();
        
        return instance.Set(key, value);
    }
}