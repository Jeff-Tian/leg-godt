using StackExchange.Redis;

namespace Cache;

public class RedisCache : CacheInstance
{
    public bool Set(string key, string value)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (env is null)
        {
            Environment.SetEnvironmentVariable("REDIS_HOST", "localhost");
            Environment.SetEnvironmentVariable("REDIS_PASSWORD", "eYVX7EwVmmxKPCDmwMtyKVge8oLd2t81");
        }
        
        var redisConnectionString = $"{Environment.GetEnvironmentVariable("REDIS_HOST")},password={Environment.GetEnvironmentVariable("REDIS_PASSWORD")}";
        
        var redis = ConnectionMultiplexer.Connect(redisConnectionString);

        var db = redis.GetDatabase();
        return db.StringSet(key, value);
    }
}