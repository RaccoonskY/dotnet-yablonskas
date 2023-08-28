using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Dotnet.Web.Attributes;
using Dotnet.Web.Data;
using Dotnet.Web.Refit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Dotnet.Web.Tests.Bl.Web;

public class ArchTests
{
    private static System.Reflection.Assembly Assembly = typeof(ICartController).Assembly;

    private static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(Assembly)
        .Build();

    public static IObjectProvider<IType> Controllers = Types().That().HaveNameEndingWith("Controller");

    [Homework(Homeworks.Bl)]
    public void Controllers_ShouldNotDependOnDbContext()
    {
        Classes()
            .That().Are(Controllers)
            .Should().NotDependOnAny(typeof(AppDbContext))
            .Check(Architecture);
    }

    [Homework(Homeworks.Bl)]
    public void Service_ShouldEnded_Service()
    {
        Types().That().ResideInNamespace("Dotnet.Web.Services")
            .Should().HaveNameEndingWith("Service")
            .Check(Architecture);
    }

    [Homework(Homeworks.Bl)]
    public void Validation_ShouldEnded_Validator()
    {
        Types().That().ResideInNamespace("Dotnet.Web.Validation")
            .Should().HaveNameEndingWith("Validator")
            .Check(Architecture);
    }

    [Homework(Homeworks.Bl)]
    public void Services_ShouldNotDependOnInterfaces()
    {
        Types().That().ResideInNamespace("Dotnet.Web.Services")
            .Should().NotDependOnAny("Dotnet.Web.Interfaces")
            .Check(Architecture);
    }

    [Homework(Homeworks.Bl)]
    public void Controllers_ShouldNotDependOnServices()
    {
        Types().That().ResideInNamespace("Dotnet.Web.Controllers")
            .Should().NotDependOnAny("Dotnet.Web.Services")
            .Check(Architecture);
    }

    [Homework(Homeworks.Bl)]
    public void Services_ShouldNotDependOnValidation()
    {
        Types().That().ResideInNamespace("Dotnet.Web.Validation")
            .Should().NotDependOnAny("Dotnet.Web.Services")
            .Check(Architecture);
    }
}