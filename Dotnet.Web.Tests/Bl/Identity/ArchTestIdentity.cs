using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Dotnet.Web.Attributes;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Dotnet.Web.Tests.Bl.Identity;

public class ArchTestIdentity
{
    private static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(System.Reflection.Assembly.Load("Dotnet.Web.Admin"))
        .Build();

    [Homework(Homeworks.Bl)]
    public void Services_ShouldEnded_Service()
    {
        Types()
            .That().ResideInNamespace("Dotnet.Web.Admin.Services")
            .Should().HaveNameEndingWith("Service")
            .Check(Architecture);
    }

    [Homework(Homeworks.Bl)]
    public void Validation_ShouldEnded_Validator()
    {
        Types()
            .That().ResideInNamespace("Dotnet.Web.Admin.Validation")
            .Should().HaveNameEndingWith("Validator")
            .Check(Architecture);
    }

    [Homework(Homeworks.Bl)]
    public void Services_ShouldNotDependOnInterfaces()
    {
        Types().That().ResideInNamespace("Dotnet.Web.Admin.Services")
            .Should().NotDependOnAny("Dotnet.Web.Admin.Interfaces")
            .Check(Architecture);
    }

    [Homework(Homeworks.Bl)]
    public void Controllers_ShouldNotDependOnServices()
    {
        Types().That().ResideInNamespace("Dotnet.Web.Admin.Controllers")
            .Should().NotDependOnAny("Dotnet.Web.Admin.Services")
            .Check(Architecture);
    }

    [Homework(Homeworks.Bl)]
    public void Services_ShouldNotDependOnValidation()
    {
        Types().That().ResideInNamespace("Dotnet.Web.Admin.Validation")
            .Should().NotDependOnAny("Dotnet.Web.Admin.Services")
            .Check(Architecture);
    }
}