using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers.Wecom;

[Route("api/wecom/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    [HttpGet("{wecomEnterpriseName}")]
    public async Task<ActionResult<Token>> GetToken(string wecomEnterpriseName)
    {
        if (wecomEnterpriseName.Equals("hardway"))
        {
            return new Token() { AccessToken = "abc" };
        }

        return BadRequest();
    }
}