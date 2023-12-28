using OneOf;
using OneOf.Types;
using Web.Controllers.Mail;
using Web.Features.Infrastructure;

namespace Web.Features.Mail;

public class MailHandler : IRequestHandler<MailCommand, OneOf<Success, Error>>
{
    public async Task<OneOf<Success, Error>> Handle(MailCommand request, CancellationToken cancellationToken)
    {
        return new Success();
    }
}
