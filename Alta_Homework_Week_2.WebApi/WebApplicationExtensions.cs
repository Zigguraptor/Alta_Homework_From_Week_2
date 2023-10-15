using Alta_Homework_Week_2.WebApi.DAL.DbContexts;
using Microsoft.Extensions.FileProviders;

namespace Alta_Homework_Week_2.WebApi;

public static class WebApplicationExtensions
{
    public static WebApplication AddLogFilesDirectoryBrowser(this WebApplication app)
    {
        var logsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        Directory.CreateDirectory(logsPath);

        app.UseDirectoryBrowser(new DirectoryBrowserOptions
        {
            FileProvider =
                new PhysicalFileProvider(logsPath),
            RequestPath = "/logs"
        });

        return app;
    }

    public static WebApplication InitSqliteDbs(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<IEmployeesShiftDbContext>();
            context.Init();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while ensuring the database is created.");
        }

        return app;
    }
}
