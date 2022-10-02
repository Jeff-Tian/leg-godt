using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Store;
using UniHeart.Wecom.RequestModels;

namespace UniHeart.Wecom;

public class Wecom
{
    private readonly WecomCorpContext _context;
    private readonly HttpClient _client;

    public Wecom(WecomCorpContext context, HttpClient? client)
    {
        _context = context;
        _client = client ?? new HttpClient();
    }

    public async Task<AccessToken> GetAccessToken(string enterprise)
    {
        var corp = await _context.WecomCorps.FirstAsync(x => x.Name.Equals(enterprise));

        if (corp == null)
        {
            throw new KeyNotFoundException($"enterprise {enterprise} not found");
        }

        var streamTask = _client.GetStreamAsync(
            $"https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={corp.CorpId}&corpsecret={corp.CorpSecret}");
        var result = await JsonSerializer.DeserializeAsync<AccessToken>(await streamTask);

        if (result == null)
        {
            throw new HttpRequestException("Failed to get access token from qyapi");
        }

        return result;
    }

    public async Task<BillListResult?> GetBillList(string wecomEnterpriseName, int beginTime = 0, int endTime = 0)
    {
        var accessToken = await GetAccessToken(wecomEnterpriseName);

        var streamTask =
            (await _client.PostAsJsonAsync(
                $"https://qyapi.weixin.qq.com/cgi-bin/externalpay/get_bill_list?access_token={accessToken.access_token}",
                new BillQuery
                {
                    begin_time = beginTime,
                    end_time = endTime
                })).Content.ReadAsStreamAsync();

        return JsonSerializer.Deserialize<BillListResult>(await streamTask);
    }

    /**
     * 查询支付信息
     *
     * 根据订单的创建时间，查找 10 分钟内指定金额的支付信息。
     */
    public async Task<IEnumerable<Bill>> GetPaymentBill(string wecomEnterpriseName, int orderCreatedAt, int cents)
    {
        var endTime = orderCreatedAt + 10 * 60;

        var res = await this.GetBillList(wecomEnterpriseName, orderCreatedAt, endTime);

        return res is null ? Array.Empty<Bill>() : res.bill_list.Where(x => x.total_fee.Equals(cents));
    }
}