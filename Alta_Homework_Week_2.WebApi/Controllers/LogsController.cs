using Microsoft.AspNetCore.Mvc;

namespace Alta_Homework_Week_2.WebApi.Controllers;

[ApiController]
[Route("logs")]
[ApiExplorerSettings(IgnoreApi = true)]
public class LogsController : ControllerBase
{
    [HttpGet("{fileName}")]
    public async Task<IActionResult> Index(string fileName)
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", fileName);
        await using var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        return File(stream, "application/octet-stream", fileName);
    }
}
