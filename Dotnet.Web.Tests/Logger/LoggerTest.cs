using Dotnet.Web.Attributes;
using Dotnet.Web.Exceptions;
using Dotnet.Web.Models;
using Dotnet.Web.Tests.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Moq;
using ILogger = Serilog.ILogger;

namespace Dotnet.Web.Tests.Logger;

public class LoggerTest : WebApplicationFactoryTestsBase
{
    public LoggerTest(DotnetWebApplicationFactory factory) : base(factory) { }

    [Homework(Homeworks.Logging)]
    public void UserFriendlyException_ShouldLogMessage()
    {
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };

        var mockLogger = new Mock<ILogger>();

        var mockException = new Mock<UserFriendlyException>();
        mockException.Setup(e => e.StackTrace)
            .Returns("Test stacktrace");
        mockException.Setup(e => e.Message)
            .Returns("Test message");
        mockException.Setup(e => e.Source)
            .Returns("Test source");

        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = mockException.Object
        };

        var filter = new UserFriendExceptionFilter(mockLogger.Object);

        filter.OnException(exceptionContext);
        mockLogger.Verify(x => x.Warning("Test message"), Times.Once);
    }
}