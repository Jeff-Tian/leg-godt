using Microsoft.EntityFrameworkCore;
using Store;
using UniHeart.Wecom;
using Web;
using Web.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddJsonConsole();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));
if (builder.Configuration["ENV"] is "test")
{
    builder.Services.AddDbContext<WecomCorpContext>(options => options.UseInMemoryDatabase("WecomCorp"));
    
    var context = builder?.Services.BuildServiceProvider().GetRequiredService<WecomCorpContext>();
    context?.WecomCorps.Add(new Corporation()
    {
        CorpId = Environment.GetEnvironmentVariable("HARDMONEY_CORP_ID") ?? string.Empty,
        CorpSecret = Environment.GetEnvironmentVariable("HARDMONEY_CORP_SECRET") ?? string.Empty,
        Name = "hardmoney"
    });
    
    context?.SaveChanges();
}
else
{
    builder.Services.AddDbContext<WecomCorpContext>(options =>
    {
        options.UseNpgsql(builder.Configuration["PostgresConnectionString"]);
    });
}

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new() { Title = "LegGodtApi", Version = "v1" }); });
builder.Services.AddSingleton(new HttpClient());
builder.Services.AddScoped<Wecom, Wecom>();
var app = builder.Build();

app.Logger.LogInformation("The leg-godt app started");
app.MapGet("/test", () => "Hello Test!");
app.MapControllers();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LegGodtApi v1"));

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

if (builder.Configuration["ENV"] is not "test")
{
    app.EnsureMigrationOfContext<WecomCorpContext>();
}

public partial class Program
{
}