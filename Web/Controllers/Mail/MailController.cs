using Microsoft.AspNetCore.Mvc;
using Web.Features.Mail;

namespace Web.Controllers.Mail;

[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerBase
{
    [HttpPost("SendEmail")]
    public async Task<string> SendEmail([FromServices] MailHandler handler, [FromBody] MailCommand command, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);
        return result.Match(success => "Success", error => "Error");
    }
}
