namespace UniHeart.Wecom.RequestModels;

public class BillQuery
{
    public int begin_time { get; set; }
    public long end_time { get; set; }
    public string? payee_userid { get; set; }
    public string? cursor { get; set; }
    public int limit { get; set; }
}