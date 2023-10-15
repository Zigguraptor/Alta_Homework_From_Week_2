using System.Text;
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
        using var fileStream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var streamReader = new StreamReader(fileStream, Encoding.UTF8);
        var text = await streamReader.ReadToEndAsync();

        return Ok(text);
    }
}
