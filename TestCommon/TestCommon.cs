using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using Store;
using Web.Models;

namespace TestCommon;

public class TestCommon
{
    public static HttpClient MockQyapiToken()
    {
        var mockCorp = Corporation.GetFakeCorp();
        
        var msgHandler = new MockHttpMessageHandler();
        
        msgHandler.When(
                $"https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={mockCorp.CorpId}&corpsecret={mockCorp.CorpSecret}")
            .Respond("application/json", @"{
    ""errcode"": 0,
        ""errmsg"": ""ok"",
        ""access_token"": ""abc"",
        ""expires_in"": 7200
    }");
        
        msgHandler.When(HttpMethod.Post, $"https://qyapi.weixin.qq.com/cgi-bin/externalpay/get_bill_list?access_token=abc").Respond("application/json", @"{
	""errcode"":0,
        ""errmsg"":""ok"",
        ""next_cursor"":""CURSOR"",
        ""bill_list"":[
        {
            ""transaction_id"":""xxxxx"",
            ""trade_state"":1,
            ""pay_time"":12345,
            ""out_trade_no"":""xxxx"",
            ""external_userid"":""xxxx"",
            ""total_fee"":100,
            ""payee_userid"":""zhangshan"",
            ""payment_type"":1,
            ""mch_id"":""123454"",
            ""remark"":""xxxx"",
            ""commodity_list"":[
            {
                ""description"":""手机"",
                ""amount"":1
            }
            ],
            ""total_refund_fee"":100,
            ""refund_list"":[
            {
                ""out_refund_no"":""xx"",
                ""refund_userid"":""xxx"",
                ""refund_comment"":""xxx"",
                ""refund_reqtime"":1605171790,
                ""refund_status"":1,
                ""refund_fee"":100
            }
            ],
            ""payer_info"":{
                ""name"":""xxx"",
                ""phone"":""xxx"",
                ""address"":""xxx""
            }
        }
        ]
    }");

        return new HttpClient(msgHandler);
    }

    public static void InjectMockCorpTo(WebApplicationFactory<Program> application)
    {
        var scope = application.Services.CreateScope();
        var wecomContext = scope.ServiceProvider.GetService<WecomCorpContext>();
        var fakeCorp = Corporation.GetFakeCorp();

        Debug.Assert(wecomContext != null, nameof(wecomContext) + " != null");

        wecomContext.WecomCorps.Add(fakeCorp);
        wecomContext.SaveChanges();
    }
}