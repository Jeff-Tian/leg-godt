using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Mail;
using Web.Controllers.Webhook.Models;
using Web.Features.Mail;

namespace Web.Controllers.Webhook;

[Route("api/webhook")]
[ApiController]
public class YuqueController
{
    private readonly ILogger<YuqueController> _logger;

    public YuqueController(ILogger<YuqueController> logger)
    {
        _logger = logger;
    }

    [HttpPost("yuque")]
    public async Task<string> Yuque([FromBody] YuqueArticle body, [FromServices] MailHandler handler,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received Yuque webhook request to {Path} with {ActionType}: {Title}", body.Data.Path,
            body.Data.Action_type, body.Data.Title);

        var tos = new string[] { "jeff.tian@outlook.com", "jie.tian@hotmail.com" };
        var mailCommand = new MailCommand(tos.ToList(), body.Data.Title,
            body.Data.Body_html ?? body.Data.Body ?? body.Data.Title);

        var result = await handler.Handle(mailCommand, cancellationToken);

        return result.Match(success => mailCommand.Body, error => "Error");
    }
}
