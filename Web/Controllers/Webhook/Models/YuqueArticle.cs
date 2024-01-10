namespace Web.Controllers.Webhook.Models;

public record YuqueArticle(YuqueArticleData Data);

public record YuqueArticleData(string? Action_type, string? Webhook_subject_type, string? Path, string? Url, string? Id, string? Slug, string? Title, string? Body, string? Body_html, string? Created_at, string? Updated_at, string? Deleted_at, string? Published_at, string? First_published_at, string? Content_updated_at);
