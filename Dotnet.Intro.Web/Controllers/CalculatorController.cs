using Dotnet.Intro.Web.middleware;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Intro.Web.Controllers
{
    [Route("/calculator")]
    [ApiController]
    public class CalculatorController : Controller
    {
        protected internal Calculator calculator = new();


        [HttpGet("add")]
        [ActionName("add")]
        public float Add(float x, float y)
        {
            Console.WriteLine("add called");
            return calculator.Add(x,y);
        }

        [HttpGet("mul")]
        [ActionName("mul")]
        public float Mul(float x, float y)
        {
            return calculator.Mul(x, y);
        }

        [HttpGet("sub")]
        [ActionName("sub")]
        public float Sub(float x, float y)
        {
            return calculator.Sub(x, y);
        }
        [HttpGet("div")]
        [ActionName("div")]
        public float Div(float x, float y)
        {
            return calculator.Div(x, y);
        }
    }
}
