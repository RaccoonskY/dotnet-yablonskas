using Microsoft.AspNetCore.Mvc.Filters;

namespace Dotnet.Web.Admin.Exceptions;

public class UserFriendExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
    }
}