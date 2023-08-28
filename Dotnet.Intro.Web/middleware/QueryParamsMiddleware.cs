namespace Dotnet.Intro.Web.middleware
{
    public class QueryParamsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] requiredQueryParameters;

        public QueryParamsMiddleware(RequestDelegate next, string requiredQueryParameters)
        {
            _next = next;
            this.requiredQueryParameters = requiredQueryParameters.Split(" ");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (requiredQueryParameters.Any(param => !context.Request.Query.ContainsKey(param)))
            {
                var res = Results.BadRequest("No required parameters");
                await res.ExecuteAsync(context);              
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }

}


