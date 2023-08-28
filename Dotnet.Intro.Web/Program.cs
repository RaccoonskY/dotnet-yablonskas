using Dotnet.Intro.Web.middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();


/*
 * First middleware to handle all unpredicted requests
 */
app.Use(async (context, next) =>
{
    await next.Invoke();
    if (context.Response.StatusCode == 404)
        await context.Response.WriteAsync("Resource Not Found");
});

/*
 * Second middleware according to the task
 */
app.Map("/middleware/hello-world", () => Results.Text("hello world!"));


var queryParams = "x y";
/*
 * Third middleware that checks the Query params
 */
app.UseWhen(context => context.Request.Path.StartsWithSegments("/calculator"),

    appBuilder => appBuilder.UseMiddleware<QueryParamsMiddleware>(queryParams));

app.MapControllers();
app.Run();

