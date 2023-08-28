using System.Reflection;
using Dotnet.Web.Attributes;
using Dotnet.Web.Controllers;
using Dotnet.Web.Data;
using Dotnet.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Dotnet.Web.Interfaces;
using Dotnet.Web.Services;
using Serilog.Events;
using Serilog;

static void ConfigureAuth(WebApplicationBuilder builder)
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, 
            new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build()
            );
        
    });
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
        AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = AuthOptions.AUDIENCE,
                ValidateLifetime = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
            };
       
        });
}

static void ConfigureValidators(WebApplicationBuilder builder)
{
}

static void ConfigureApi(WebApplicationBuilder builder)
{
    builder.Services.AddControllersWithViews();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "JWT Authorization header using the Bearer scheme."
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
            },
            new string[] {}
        }
    });
    });
}

static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddEntityFrameworkNpgsql();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ICommentService, CommentService>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<ICartService, CartService>();
    builder.Services.AddScoped<IOrderService, OrderService>();
}


static void ConfigureIdentity(WebApplicationBuilder builder)
{
    builder.Services
        .AddIdentity<User, UserRole>()
        .AddEntityFrameworkStores<AppDbContext>();   
}

static void ConfigureDb(WebApplicationBuilder builder)
{
    var env = builder.Environment;
    if (env.IsDevelopment() || env.IsProduction())
    {
        builder.Services.AddDbContext<AppDbContext>((sp, options) =>
                 options.UseNpgsql(
                     builder.Configuration.GetValue<string>(
                         "ConnectionStrings:Default"
                     )
                 ).UseInternalServiceProvider(sp)
           );

    }
    else if (env.IsEnvironment("Test"))
    {
        builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase($"Temp"));
    }
    
    builder.Services.AddScoped<DbContext>(x => x.GetRequiredService<AppDbContext>());
}

static void ConfigureLogger(WebApplicationBuilder builder)
{
    Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();
    builder.Host.UseSerilog(Log.Logger);
    

}

static void InitDb(WebApplication app)
{
    if(bool.TryParse(app.Configuration["Database:SkipInitialization"], out var skip))
    {
        if (skip)
        {
            return;
        }
    }
    
    using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (context.Database.IsRelational())
    {
        context.Database.Migrate();
    }

    DbSeeder.Seed(context, 
        serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>(),
        serviceScope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>());
}

static void RunApp(WebApplicationBuilder builder)
{
    var app = builder.Build();
    app.UseSerilogRequestLogging();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "version 1"));

    app.UseCors(x => x.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
    );

    
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    var hw = typeof(DotnetControllerBase)
        .Assembly
        .GetCustomAttribute<HomeworkProgressAttribute>()?.Number;
    if (hw > 4)
    {
        InitDb(app);
    }
   

    app.Run();
}

var b = WebApplication.CreateBuilder(args);


ConfigureApi(b);    
ConfigureDb(b);
ConfigureIdentity(b);
ConfigureServices(b);
ConfigureValidators(b);
ConfigureLogger(b);
ConfigureAuth(b);
RunApp(b);

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}