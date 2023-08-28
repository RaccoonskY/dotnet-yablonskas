using Dotnet.Web.Attributes;
using Dotnet.Web.Data;
using Dotnet.Web.Tests.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Web.Tests.Configuration;

public class ConfigurationTests : WebApplicationFactoryTestsBase
{
    public ConfigurationTests(DotnetWebApplicationFactory factory) : base(factory) { }

    [HomeworkTheory(Homeworks.Config)]
    [InlineData("Development", false)]
    [InlineData("Test", true)]
    [InlineData("Production", false)]
    public void DbProviderAndConnectionString_VaryByEnvironment(string env, bool shouldBeInMemory)
    {
        IConfigurationRoot? root = null;
        var isInMemory = false;
        
        var _ = Factory
            .WithWebHostBuilder(b =>
            {
                b.UseEnvironment(env);
                b.ConfigureAppConfiguration(cb =>
                {
                    var data = new Dictionary<string, string>
                    {
                        { "Database:SkipInitialization", "true" }
                    };
                    cb.AddInMemoryCollection(data!);
                    root = cb.Build();
                });
                b.ConfigureServices(s =>
                {
                    var sp = s.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var dbc = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    isInMemory = dbc.Database.IsInMemory();
                });
            })
            .CreateClient();

        var defaultConnectionString = root.NotNull()["ConnectionStrings:Default"];
        if (!shouldBeInMemory)
        {
            defaultConnectionString.Should().NotBeEmpty();
            isInMemory.Should().BeFalse();
        }
        else
        {
            defaultConnectionString.Should().BeNullOrEmpty();
            isInMemory.Should().BeTrue();
        }
    }
}