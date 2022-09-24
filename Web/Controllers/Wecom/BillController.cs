using Microsoft.AspNetCore.Mvc;
using Store;
using UniHeart.Wecom;

namespace Web.Controllers.Wecom;

[Route("api/wecom/[controller]")]
[ApiController]
public class BillController : ControllerBase {
    [HttpGet("{wecomEnterpriseName}")]
    public async Task<string> GetBillList(string wecomEnterpriseName) {
        return "hello " + wecomEnterpriseName;
    }
}