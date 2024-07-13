using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Store;
using UniHeart.Wecom;
using Web;
using Web.Features.Infrastructure;
using Web.Models;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web application...");

    var builder = WebApplication.CreateBuilder(args);

    // Configure Serilog
    builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext();
    });

    builder.Logging.AddJsonConsole();

    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddControllers();
    builder.Services.AddDbContext<TodoContext>(opt =>
        opt.UseInMemoryDatabase("TodoList"));

    builder.Services.AddConfiguredRequestHandlers();

    if (builder.Configuration["ASPNETCORE_ENVIRONMENT"] is "Test")
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

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
            { Title = "LegGodtApi", Version = "v1" });
    });
    builder.Services.AddSingleton(new HttpClient());
    builder.Services.AddSingleton<IAmazonSimpleEmailService>(
        new AmazonSimpleEmailServiceClient(new EnvironmentVariablesAWSCredentials(), RegionEndpoint.USEast1));
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

    // Obtain a logger instance
    var logger = app.Services.GetRequiredService<ILogger<Program>>();

    // Log a message indicating the start of the application
    logger.LogInformation("The application has started.");

    if (builder.Configuration["ENV"] is not "Test")
    {
        app.EnsureMigrationOfContext<WecomCorpContext>();
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program
{
}
