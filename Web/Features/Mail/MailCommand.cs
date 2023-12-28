namespace Web.Controllers.Mail;

public record MailCommand(string To, string Subject, string Body);
