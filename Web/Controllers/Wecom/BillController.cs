using Microsoft.AspNetCore.Mvc;
using UniHeart.Wecom;

namespace Web.Controllers.Wecom;

[Route("api/wecom/[controller]")]
[ApiController]
public class BillController : ControllerBase
{
    private readonly UniHeart.Wecom.Wecom _wecom;

    public BillController(UniHeart.Wecom.Wecom wecom)
    {
        _wecom = wecom;
    }

    [HttpGet("{wecomEnterpriseName}")]
    public async Task<BillListResult?> GetBillList(string wecomEnterpriseName)
    {
        return await _wecom.GetBillList(wecomEnterpriseName);
    }

    [HttpGet("{wecomEnterpriseName}/{orderCreatedAt:int}")]
    public async Task<IEnumerable<Bill>> GetPaymentsInfo(string wecomEnterpriseName, int orderCreatedAt, int cents)
    {
        return await _wecom.GetPaymentBill(wecomEnterpriseName, orderCreatedAt, cents);
    }
}