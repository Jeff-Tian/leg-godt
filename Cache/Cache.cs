namespace Cache;

public static class Cache
{
    private static string _cache = "";

    public static string Set(string key, string value)
    {
        _cache = value;
        return "success";
    }
}