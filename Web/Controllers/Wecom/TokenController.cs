using Microsoft.AspNetCore.Mvc;
using Store;
using UniHeart.Wecom;

namespace Web.Controllers.Wecom;

[Route("api/wecom/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly WecomCorpContext _context;

    public TokenController(WecomCorpContext context)
    {
        _context = context;
    }

    [HttpGet("{wecomEnterpriseName}")]
    public async Task<AccessToken> GetToken(string wecomEnterpriseName)
    {
        var wecom = new UniHeart.Wecom.Wecom(_context, null);
        return await wecom.GetAccessToken(wecomEnterpriseName);
    }
}