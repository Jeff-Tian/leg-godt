namespace UniHeart.Wecom;

public class AccessToken
{
    public int errcode { get; set; }
    public string errmsg { get; set; }
    public string access_token { get; set; }
    public int expires_in { get; set; }
}