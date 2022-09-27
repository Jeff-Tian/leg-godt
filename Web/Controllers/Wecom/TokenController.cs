using Microsoft.AspNetCore.Mvc;
using Store;
using UniHeart.Wecom;

namespace Web.Controllers.Wecom;

[Route("api/wecom/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly WecomCorpContext _context;
    private readonly HttpClient _client;

    public TokenController(WecomCorpContext context, HttpClient client)
    {
        _context = context;
        _client = client;
    }

    [HttpGet("{wecomEnterpriseName}")]
    public async Task<AccessToken> GetToken(string wecomEnterpriseName)
    {
        var wecom = new UniHeart.Wecom.Wecom(_context, _client);
        return await wecom.GetAccessToken(wecomEnterpriseName);
    }
}