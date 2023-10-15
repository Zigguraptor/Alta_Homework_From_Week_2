using Microsoft.Extensions.FileProviders;

namespace Alta_Homework_Week_2.WebApi;

public static class WebApplicationExtensions
{
    public static WebApplication AddLogFilesDirectoryBrowser(this WebApplication app)
    {
        app.UseDirectoryBrowser(new DirectoryBrowserOptions
        {
            FileProvider =
                new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs")),
            RequestPath = "/logs"
        });

        return app;
    }
}
