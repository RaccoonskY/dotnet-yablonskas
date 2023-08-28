using Microsoft.AspNetCore.Mvc.Filters;
using ILogger = Serilog.ILogger;

namespace Dotnet.Web.Exceptions;

public class UserFriendExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger _logger;

    public UserFriendExceptionFilter(ILogger logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        _logger.Warning(context.Exception.Message);
    }
}