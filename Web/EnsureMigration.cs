using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Web;

public static class EnsureMigration
{
    public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
    {
        try
        {
            var context = app.ApplicationServices.GetService<T>();

            Debug.Assert(context != null, nameof(context) + " != null");

            // context.Database.EnsureCreated();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

}