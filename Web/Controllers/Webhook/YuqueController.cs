using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Webhook;

[Route("api/webhook")]
[ApiController]
public class YuqueController
{
    [HttpPost("yuque")]
    public async Task<string> Yuque([FromBody] object body)
    {
        return "OK";
    }
}
