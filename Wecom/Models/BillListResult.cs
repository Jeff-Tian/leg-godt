namespace UniHeart.Wecom;

public class BillListResult
{
    public int errcode { get; set; }
    public string errmsg { get; set; }
    public string next_cursor { get; set; }
    public Bill[] bill_list { get; set; }
}

public class Bill
{
    public string transaction_id { get; set; }
    public int trade_state { get; set; }
    public int pay_time { get; set; }
    public string out_trade_no { get; set; }
    public string external_userid { get; set; }
    public int total_fee { get; set; }
    public string payee_userid { get; set; }
    public int payment_type { get; set; }
    public string mch_id { get; set; }
    public string remark { get; set; }
    public Commodity[] commodity_list { get; set; }
    public int total_refund_fee { get; set; }
    public Refund[] refund_list { get; set; }
    public PayerInfo payer_info { get; set; }
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