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

    public async Task<BillListResult?> GetBillList(string wecomEnterpriseName)
    {
        var accessToken = await GetAccessToken(wecomEnterpriseName);

        var streamTask =
            (await _client.PostAsJsonAsync(
                $"https://qyapi.weixin.qq.com/cgi-bin/externalpay/get_bill_list?access_token={accessToken.access_token}",
                new BillQuery()
                {
                    begin_time = 978278400,
                    end_time = 4133952000
                })).Content.ReadAsStreamAsync();

        return JsonSerializer.Deserialize<BillListResult>(await streamTask);
    }
}