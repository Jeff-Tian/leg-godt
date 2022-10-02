using Microsoft.AspNetCore.Mvc;
using Store;
using UniHeart.Wecom;

namespace Web.Controllers.Wecom;

[Route("api/wecom/[controller]")]
[ApiController]
public class BillController : ControllerBase
{
    private readonly HttpClient _client;
    private readonly WecomCorpContext _context;

    public BillController(HttpClient client, WecomCorpContext context)
    {
        _client = client;
        _context = context;
    }

    [HttpGet("{wecomEnterpriseName}")]
    public async Task<BillListResult?> GetBillList(string wecomEnterpriseName)
    {
        var wecom = new UniHeart.Wecom.Wecom(_context, _client);
        return await wecom.GetBillList(wecomEnterpriseName);
    }

    [HttpGet("{wecomEnterpriseName}/{orderCreatedAt:int}")]
    public async Task<IEnumerable<Bill>> GetPaymentsInfo(string wecomEnterpriseName, int orderCreatedAt, int cents)
    {
        var wecom = new UniHeart.Wecom.Wecom(_context, _client);
        return await wecom.GetPaymentBill(wecomEnterpriseName, orderCreatedAt, cents);
    }
}