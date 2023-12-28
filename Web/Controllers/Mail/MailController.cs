using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Mail;

[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerBase
{
    [HttpPost("SendEmail")]
    public async Task<string> SendEmail()
    {
        return "Hello";
    }
}
