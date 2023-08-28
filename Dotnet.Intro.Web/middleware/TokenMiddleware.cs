namespace Dotnet.Intro.Web.middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate next;

        public TokenMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (token == "1234")
            {
                await context.Response.WriteAsync("Token is valid! Hello World!");
            }
            else if (string.IsNullOrWhiteSpace(token))
            {
                context.Response.StatusCode = 403;
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
