using Dotnet.Web.Attributes;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Dotnet.Web.Tests.Docker;

public class DockerTests
{
    [Homework(Homeworks.Docker)]
    public void ConnectionStringsDefault_IsNotNullInDockerCompose()
    {
        var filePath = "../../../../docker-compose.yml";
        if (!File.Exists(filePath))
        {
            throw new InvalidOperationException();
        }

        var b = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .WithNamingConvention(HyphenatedNamingConvention.Instance);
        
        var dc = b.Build().Deserialize<DockerCompose>(File.ReadAllText(filePath));
        
        Assert.NotNull(dc.Services?.DotnetWeb?.Environment?.GetValueOrDefault("ConnectionStrings:Default"));
    }
}