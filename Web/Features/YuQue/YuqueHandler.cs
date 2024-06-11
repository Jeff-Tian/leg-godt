using OneOf;
using OneOf.Types;
using Web.Controllers.Webhook.Models;
using Web.Features.Infrastructure;

namespace Web.Features.YuQue;

public class YuqueHandler : IRequestHandler<StrapiEntry, OneOf<Success, Error>>
{
    private readonly ILogger<YuqueHandler> _logger;
    private readonly HttpClient _client;

    public YuqueHandler(ILogger<YuqueHandler> logger, HttpClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task<OneOf<Success, Error>> Handle(StrapiEntry body, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received Strapi webhook request to {Event} with {Model}: {FullName}", body.Event,
            body.Model, body.Entry.Full_name);

        var response = await _client.PostAsJsonAsync("https://api.yuque.com/v2/repos/tian-jie/docs", new Dictionary<string, object>
        {
            {"title", body.Entry.Full_name},
            {"body", $"Created at {body.Entry.CreatedAt}"},
            {"format", "markdown"},
            {"public", 0},
        }, cancellationToken);

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
