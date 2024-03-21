using Microsoft.AspNetCore.Mvc;
using Web.Features.Mail;

namespace Web.Controllers.Mail;

[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerBase
{
    private readonly ILogger<MailController> _logger;

    public MailController(ILogger<MailController> logger)
    {
        _logger = logger;
    }

    [HttpPost("SendEmail")]
    public async Task<string> SendEmail([FromServices] MailHandler handler, [FromBody] MailCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received email request to {To} with {Subject}: {Body}", string.Join(',', command.Tos),
            command.Subject, command.Body);

        var result = await handler.Handle(command, cancellationToken);
        return result.Match(success => "Success", error => "Error");
    }
}
