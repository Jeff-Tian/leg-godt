namespace Web.Controllers.Webhook.Models;

public record StrapiEntry(string Event, string CreatedAt, string Model, StrapiEntryData Entry);

public record StrapiEntryData(int Id, string City, string Full_name, string CreatedAt, string UpdatedAt);
