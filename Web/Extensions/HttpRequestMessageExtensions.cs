using System.Text;

namespace Web;

public static class HttpRequestMessageExtensions
{
    public async static Task<string> ToCurlCommand(this HttpRequestMessage request)
    {
        var command = new StringBuilder();
        command.Append("curl ");
        command.Append($"-X {request.Method.Method} ");
        command.Append($"\"{request.RequestUri}\" ");

        foreach (var header in request.Headers)
        {
            command.Append($"-H \"{header.Key}: {string.Join(", ", header.Value)}\" ");
        }

        if (request.Content is not null)
        {
            var contentType = request.Content.Headers.ContentType?.MediaType;
            if (contentType is not null)
            {
                command.Append($"-H \"Content-Type: {contentType}\" ");
            }

            var content = await request.Content.ReadAsStringAsync();

            command.Append($"-d '{content}' ");
        }

        return command.ToString();
    }
}
