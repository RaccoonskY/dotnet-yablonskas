using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace Dotnet.Web.Controllers;

public class LogController : DotnetControllerBase
{
    private readonly ILogger _logger;

    public LogController (ILogger logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Log(string text)
    {
        _logger.Information(text);
        return Ok();
    }
}