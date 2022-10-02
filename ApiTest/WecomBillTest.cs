using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTest;

[TestClass]
public class WecomBillTest
{
    [TestInitialize]
    public void Setup()
    {
        Environment.SetEnvironmentVariable("ENV", "test");
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
                services.AddSingleton(TestCommon.TestCommon.MockQyapiToken()));
        });

        TestCommon.TestCommon.InjectMockCorpTo(application);

        client = application.CreateClient();
    }

    HttpClient client;

    [TestMethod]
    public async Task TestGetBillListOk()
    {
        var res = await client.GetAsync("/api/wecom/Bill/hardway");
        Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        var body = await res.Content.ReadAsStringAsync();
        Assert.AreEqual(
            @"{""errcode"":0,""errmsg"":""ok"",""next_cursor"":""CURSOR"",""bill_list"":[{""transaction_id"":""xxxxx"",""trade_state"":1,""pay_time"":12345,""out_trade_no"":""xxxx"",""external_userid"":""xxxx"",""total_fee"":100,""payee_userid"":""zhangshan"",""payment_type"":1,""mch_id"":""123454"",""remark"":""xxxx"",""commodity_list"":[{""description"":""手机"",""amount"":1}],""total_refund_fee"":100,""refund_list"":[{""out_refund_no"":""xx"",""refund_userid"":""xxx"",""refund_comment"":""xxx"",""refund_reqtime"":1605171790,""refund_status"":1,""refund_fee"":100}],""payer_info"":{""name"":""xxx"",""phone"":""xxx"",""address"":""xxx""}}]}",
            body);
    }

    [TestMethod]
    public async Task TestGetPaymentEmpty()
    {
        var res = await client.GetAsync("/api/wecom/Bill/hardway/12345?cents=10");
        Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        var body = await res.Content.ReadAsStringAsync();
        Assert.AreEqual(@"[]", body);
    }

    [TestMethod]
    public async Task TestGetPayment1Payment()
    {
        var res = await client.GetAsync("/api/wecom/Bill/hardway/12345?cents=100");
        Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        var body = await res.Content.ReadAsStringAsync();
        Assert.AreEqual(@"[{""transaction_id"":""xxxxx"",""trade_state"":1,""pay_time"":12345,""out_trade_no"":""xxxx"",""external_userid"":""xxxx"",""total_fee"":100,""payee_userid"":""zhangshan"",""payment_type"":1,""mch_id"":""123454"",""remark"":""xxxx"",""commodity_list"":[{""description"":""手机"",""amount"":1}],""total_refund_fee"":100,""refund_list"":[{""out_refund_no"":""xx"",""refund_userid"":""xxx"",""refund_comment"":""xxx"",""refund_reqtime"":1605171790,""refund_status"":1,""refund_fee"":100}],""payer_info"":{""name"":""xxx"",""phone"":""xxx"",""address"":""xxx""}}]", body);
    }
}