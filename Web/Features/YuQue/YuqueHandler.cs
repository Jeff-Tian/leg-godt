using System.Diagnostics;
using System.Net.Http.Headers;
using OneOf;
using OneOf.Types;
using Web.Controllers.Webhook.Models;
using Web.Features.Infrastructure;

namespace Web.Features.YuQue;

public class YuqueHandler(ILogger<YuqueHandler> logger, HttpClient client, IConfiguration configuration)
    : IRequestHandler<StrapiEntry, OneOf<Success, Error>>
{
    public async Task<OneOf<Success, Error>> Handle(StrapiEntry body, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received Strapi webhook request to {Event} with {Model}: {FullName}", body.Event,
            body.Model, body.Entry.Full_name);

        var yuqueToken = configuration["YuQue:Token"];
        Debug.Assert(yuqueToken is not null);
        var yuqueBookId = configuration["YuQue:BookId"];
        Debug.Assert(yuqueBookId is not null);

        var request = new HttpRequestMessage(HttpMethod.Post,
            $"https://www.yuque.com/api/v2/repos/{yuqueBookId}/docs");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", yuqueToken);
        request.Headers.Add("X-Auth-Token", yuqueToken);
        request.Headers.UserAgent.ParseAdd("LegGodt/1.0 (+https://leg-godt.azurewebsites.net/)");
        request.Content = JsonContent.Create(new Dictionary<string, object?>
        {
            { "title", body.Entry.Title ?? body.Entry.Full_name },
            { "body", body.Entry.Content ?? $"Created at {body.Entry.CreatedAt}" },
            { "format", "markdown" },
            { "public", 0 },
            { "status", 0 },
        });

        logger.LogInformation("Sending Yuque request:\n{CurlCommand}", await request.ToCurlCommand());

        var response = await client.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            logger.LogInformation("Created Yuque note for {FullName}", body.Entry.Full_name);
            return new Success();
        }

        logger.LogError("Error creating Yuque note for {FullName}: {StatusCode}\n{Content}", body.Entry.Full_name,
            response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        return new Error();
    }
}
