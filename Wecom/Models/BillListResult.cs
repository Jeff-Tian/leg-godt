namespace UniHeart.Wecom;

public class BillListResult
{
    public int errcode { get; set; }
    public string errmsg { get; set; }
    public string next_cursor { get; set; }
    public Bill[] bill_list { get; set; }
}

public class Commodity
{
    public string description { get; set; }
    public int amount { get; set; }
}

public class Refund
{
    public string out_refund_no { get; set; }
    public string refund_userid { get; set; }
    public string refund_comment { get; set; }
    public int refund_reqtime { get; set; }
    public int refund_status { get; set; }
    public int refund_fee { get; set; }
}

public class PayerInfo
{
    public string name { get; set; }
    public string phone { get; set; }
    public string address { get; set; }
}