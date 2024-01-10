using OneOf;
using OneOf.Types;
using Web.Controllers.Mail;
using Web.Features.Infrastructure;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace Web.Features.Mail;

public class MailHandler : IRequestHandler<MailCommand, OneOf<Success, Error>>
{
    private readonly ILogger<MailHandler> _logger;
    private readonly IAmazonSimpleEmailService _emailClient;

    public MailHandler(ILogger<MailHandler> logger, IAmazonSimpleEmailService emailClient)
    {
        _logger = logger;
        _emailClient = emailClient;
    }

    public async Task<OneOf<Success, Error>> Handle(MailCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending email to {To} with {Body}", request.To, request.Body);

        try
        {
            var sendRequest = new SendEmailRequest
            {
                Source = "Jeff Tian <jeff.tian@outlook.com>", Destination = new Destination(), Message = new Message()
            };
            sendRequest.Destination.ToAddresses.Add(request.To);
            sendRequest.Message.Subject = new Content(request.Subject);
            sendRequest.Message.Body = new Body(new Content(request.Body));
            await _emailClient.SendEmailAsync(sendRequest, cancellationToken);

            _logger.LogInformation("Email sent to {To}", request.To);
            return new Success();
        } catch (Exception ex)
        {
            _logger.LogError("Error sending email to {To}: {Message}\n{Stack}", request.To, ex.Message, ex.StackTrace);
            return new Error();
        }
    }
}
