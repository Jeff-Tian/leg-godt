using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;

namespace Web.Pages;

public class IndexModel : PageModel
{

    public string OsVersion => RuntimeInformation.OSDescription;
    public string? AspNetCoreEnvironment;

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        AspNetCoreEnvironment = configuration["ASPNETCORE_ENVIRONMENT"];
    }

    public void OnGet()
    {        
    }
}
