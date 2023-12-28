using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Mail;
using Web.Controllers.Webhook.Models;
using Web.Features.Mail;

namespace Web.Controllers.Webhook;

[Route("api/webhook")]
[ApiController]
public class YuqueController
{
    [HttpPost("yuque")]
    public async Task<string> Yuque([FromBody] YuqueArticle body, [FromServices] MailHandler handler, CancellationToken cancellationToken)
    {
        var mailCommand = new MailCommand("jeff.tian@outlook.com", body.Data.Title, body.Data.BodyHtml);

        var result = await handler.Handle(mailCommand, cancellationToken);

        return result.Match(success => "Success", error => "Error");
    }
}
