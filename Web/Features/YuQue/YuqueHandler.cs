using System.Diagnostics;
using System.Net.Http.Headers;
using OneOf;
using OneOf.Types;
using Web.Controllers.Webhook.Models;
using Web.Features.Infrastructure;

namespace Web.Features.YuQue;

public class YuqueHandler : IRequestHandler<StrapiEntry, OneOf<Success, Error>>
{
    private readonly ILogger<YuqueHandler> _logger;
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;

    public YuqueHandler(ILogger<YuqueHandler> logger, HttpClient client, IConfiguration configuration)
    {
        _logger = logger;
        _client = client;
        _configuration = configuration;
    }

    public async Task<OneOf<Success, Error>> Handle(StrapiEntry body, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received Strapi webhook request to {Event} with {Model}: {FullName}", body.Event,
            body.Model, body.Entry.Full_name);

        var yuqueToken = _configuration["YuQue:Token"];
        Debug.Assert(yuqueToken is not null);
        var yuqueBookId = _configuration["YuQue:BookId"];
        Debug.Assert(yuqueBookId is not null);

        var request = new HttpRequestMessage(HttpMethod.Post,
            $"https://www.yuque.com/api/v2/repos/{yuqueBookId}/docs");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", yuqueToken);
        request.Headers.Add("X-Auth-Token", yuqueToken);
        request.Headers.UserAgent.ParseAdd("LegGodt/1.0 (+https://leg-godt.azurewebsites.net/)");
        request.Content = JsonContent.Create(new Dictionary<string, object>
        {
            { "title", body.Entry.Full_name },
            { "body", $"Created at {body.Entry.CreatedAt}" },
            { "format", "markdown" },
            { "public", 0 },
            { "status", 0 },
        });

        _logger.LogInformation("Sending Yuque request:\n{CurlCommand}", await request.ToCurlCommand());

        var response = await _client.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Created Yuque note for {FullName}", body.Entry.Full_name);
            return new Success();
        }

        _logger.LogError("Error creating Yuque note for {FullName}: {StatusCode}\n{Content}", body.Entry.Full_name,
            response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        return new Error();
    }
}
