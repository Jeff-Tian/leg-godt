namespace Web.Controllers.Mail;

public record MailCommand(List<string> Tos, string Subject, string Body);
