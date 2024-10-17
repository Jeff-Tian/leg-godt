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
        var proxyResult1 =
            await ProxyTo(request, "http://8.217.210.211:3001/yuque/webhook", cancellationToken);
        var proxyResult2 = await ProxyTo(request,
            "https://leg-godt.azurewebsites.net/api/mail/SendEmail", cancellationToken);

        return ((OneOf<Success, Error>[]) [proxyResult1, proxyResult2]).All(result => result.Value is Success)
            ? new Success()
            : new Error();
    }

    private async Task<OneOf<Success, Error>> ProxyTo(YuqueArticle request, string targetUrl,
        CancellationToken cancellationToken)
    {
        OneOf<Success, Error> oneOf;
        logger.LogInformation("Received Yuque webhook request to {Path} with {ActionType}: {Title}", request.Data.Path,
            request.Data.Action_type, request.Data.Title);

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, targetUrl);
        httpRequest.Headers.UserAgent.ParseAdd("LegGodt/1.0 (+https://leg-godt.azurewebsites.net/)");
        httpRequest.Content = JsonContent.Create(request);

        logger.LogInformation("Sending Yuque request:\n{CurlCommand}", await httpRequest.ToCurlCommand());

        var response = await client.SendAsync(httpRequest, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            logger.LogInformation("Proxy request to {Path} with {ActionType}: {Title} succeeded", request.Data.Path,
                request.Data.Action_type, request.Data.Title);
            {
                return new Success();
            }
        }

        logger.LogError("Error proxying request to {Path} with {ActionType}: {Title}: {StatusCode}\n{Content}",
            request.Data.Path, request.Data.Action_type, request.Data.Title, response.StatusCode,
            await response.Content.ReadAsStringAsync(cancellationToken));

        return new Error();
    }
}
