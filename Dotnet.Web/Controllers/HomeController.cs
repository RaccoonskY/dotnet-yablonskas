using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Web.Controllers;

public class HomeController: ControllerBase
{
    [HttpGet("/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Index() => Redirect("/swagger");
   

}