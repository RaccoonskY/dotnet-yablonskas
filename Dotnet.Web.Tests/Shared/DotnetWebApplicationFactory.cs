using Dotnet.Web.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Dotnet.Web.Tests.Shared;

public class DotnetWebApplicationFactory : WebApplicationFactory<DotnetControllerBase>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
    }
}