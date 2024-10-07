using OneOf;
using OneOf.Types;
using Web.Controllers.Webhook.Models;
using Web.Features.Infrastructure;

namespace Web.Features.Proxy;

public class ProxyHandler(ILogger<ProxyHandler> logger, HttpClient client)
    : IRequestHandler<YuqueArticle, OneOf<Success, Error>>
{
    public async Task<OneOf<Success, Error>> Handle(YuqueArticle request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received Yuque webhook request to {Path} with {ActionType}: {Title}", request.Data.Path,
            request.Data.Action_type, request.Data.Title);

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"http://8.217.210.211:3001/yuque/webhook");
        httpRequest.Headers.UserAgent.ParseAdd("LegGodt/1.0 (+https://leg-godt.azurewebsites.net/)");
        httpRequest.Content = JsonContent.Create(request);

        logger.LogInformation("Sending Yuque request:\n{CurlCommand}", await httpRequest.ToCurlCommand());

        var response = await client.SendAsync(httpRequest, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            logger.LogInformation("Proxy request to {Path} with {ActionType}: {Title} succeeded", request.Data.Path,
                request.Data.Action_type, request.Data.Title);
            return new Success();
        }

        logger.LogError("Error proxying request to {Path} with {ActionType}: {Title}: {StatusCode}\n{Content}",
            request.Data.Path, request.Data.Action_type, request.Data.Title, response.StatusCode,
            await response.Content.ReadAsStringAsync(cancellationToken));

        return new Error();
    }
}
