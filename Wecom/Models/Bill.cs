namespace UniHeart.Wecom;

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