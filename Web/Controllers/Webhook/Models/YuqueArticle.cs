namespace Web.Controllers.Webhook.Models;

public record YuqueArticle(YuqueArticleData Data);

public record YuqueArticleData(string? ActionType, string? WebhookSubjectType, string? Path, string? Url, string? Id, string? Slug, string? Title, string? Body, string? BodyHtml, string? CreatedAt, string? UpdatedAt, string? DeletedAt, string? PublishedAt, string? FirstPublishedAt, string? ContentUpdatedAt);
