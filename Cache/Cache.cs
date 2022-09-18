using StackExchange.Redis;

namespace Cache;

public static class Cache
{
    public static bool Set(string key, string value)
    {
        var redis = ConnectionMultiplexer.Connect("localhost,password=eYVX7EwVmmxKPCDmwMtyKVge8oLd2t81");
        var db = redis.GetDatabase();
        return db.StringSet(key, value);
    }
}