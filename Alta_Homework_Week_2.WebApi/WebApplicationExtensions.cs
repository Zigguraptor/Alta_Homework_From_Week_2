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
}
