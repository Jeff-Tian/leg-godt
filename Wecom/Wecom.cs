namespace Wecom;

public class Wecom
{
    public static string GetAccessToken(string enterprise)
    {
        if (enterprise == "hardway")
        {
            return "abc";
        }

        throw new ArgumentException($"{enterprise} is not supported yet.");
    }
}