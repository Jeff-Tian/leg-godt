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
        try
        {
            Console.WriteLine($@"GetAccessToken for {enterprise}...");
            var corp = await _context.WecomCorps.FirstOrDefaultAsync(x => x.Name.Equals(enterprise));

            if (corp == null)
            {
                throw new KeyNotFoundException($"enterprise {enterprise} not found");
            }

            Console.WriteLine($"Corp = {corp?.Name}");
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={corp.CorpId}&corpsecret={corp.CorpSecret}";
            Console.WriteLine($"Getting its access token... {url}");

            var streamTask = _client.GetStreamAsync(url);
            var resolvedStream = await streamTask;
            var result = await JsonSerializer.DeserializeAsync<AccessToken>(resolvedStream);
            
            Console.WriteLine($"Result of access token = {result}");

            if (result == null)
            {
                throw new HttpRequestException("Failed to get access token from qyapi");
            }

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Met exception: {ex.Message}");
            throw;
        }
    }

    public async Task<BillListResult?> GetBillList(string wecomEnterpriseName, int beginTime = 0, int endTime = 0)
    {
        var accessToken = await GetAccessToken(wecomEnterpriseName);

        var url =
            $"https://qyapi.weixin.qq.com/cgi-bin/externalpay/get_bill_list?access_token={accessToken.access_token}";
        var streamTask =
            (await _client.PostAsJsonAsync(
                url,
                new BillQuery
                {
                    begin_time = beginTime,
                    end_time = endTime
                })).Content.ReadAsStreamAsync();

        var res = JsonSerializer.Deserialize<BillListResult>(await streamTask);
        
        Console.WriteLine($@"wecom res = {res}");
        
        return res;
    }

    /**
     * 查询支付信息
     *
     * 根据订单的创建时间，查找 10 分钟内指定金额的支付信息。
     * 由于微信服务器的时间可能和本服务器的时间不一致，所以开始时间往前推 1 分钟，结束时间往后推 9 分钟。
     */
    public async Task<IEnumerable<Bill>> GetPaymentBill(string wecomEnterpriseName, int orderCreatedAt, int cents)
    {
        var beginTime = orderCreatedAt - 60;
        var endTime = orderCreatedAt + 9 * 60;

        var res = await GetBillList(wecomEnterpriseName, beginTime, endTime);

        return res is null ? Array.Empty<Bill>() : res.bill_list.Where(x => x.total_fee.Equals(cents));
    }
}