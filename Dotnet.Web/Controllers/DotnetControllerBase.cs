using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Web.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class DotnetControllerBase : Controller
{
}