using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Mail;
using Web.Controllers.Webhook.Models;
using Web.Features.Mail;
using Web.Features.Proxy;
using Web.Features.YuQue;

namespace Web.Controllers.Webhook;

[Route("api/webhook")]
[ApiController]
public class YuqueController(ILogger<YuqueController> logger)
{
    [HttpPost("yuque")]
    public async Task<string> Yuque([FromBody] YuqueArticle body, [FromServices] MailHandler handler,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Received Yuque webhook request to {Path} with {ActionType}: {Title}", body.Data.Path,
            body.Data.Action_type, body.Data.Title);

        var tos = new string[] { "jeff.tian@outlook.com", "jie.tian@hotmail.com" };
        var mailCommand = new MailCommand(tos.ToList(), body.Data.Title,
            body.Data.Body_html ?? body.Data.Body ?? body.Data.Title);

        var result = await handler.Handle(mailCommand, cancellationToken);

        return result.Match(success => mailCommand.Body, error => "Error");
    }

    [HttpPost("strapi")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> Strapi(
        [FromBody] StrapiEntry body,
        [FromServices] YuqueHandler handler,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Received Strapi webhook request to {Event} with {Model}: {FullName}", body.Event,
            body.Model, body.Entry.Full_name);

        var result = await handler.Handle(body, cancellationToken);

        return result.Match(success => "Success", error => "Error");
    }

    [HttpPost("proxy")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> Proxy(
        [FromBody] YuqueArticle body,
        [FromServices] ProxyHandler handler,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Received Yuque webhook request to {Path} with {ActionType}: {Title}", body.Data.Path,
            body.Data.Action_type, body.Data.Title);

        var result = await handler.Handle(body, cancellationToken);

        return result.Match(success => "Success", error => "Error");
    }
}
