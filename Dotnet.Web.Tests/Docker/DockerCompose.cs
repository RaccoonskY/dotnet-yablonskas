namespace Dotnet.Web.Tests.Docker;

public class DockerCompose
{
    public ServicesSection? Services { get; set; }
}

public class ServicesSection
{
    public DotnetWeb? DotnetWeb { get; set; }
}

public class DotnetWeb
{
    public Dictionary<string, string>? Environment { get; set; }
}