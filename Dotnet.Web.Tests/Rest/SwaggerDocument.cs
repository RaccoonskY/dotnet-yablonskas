namespace Dotnet.Web.Tests.Rest;

public class SwaggerDocument
{
    public string? OpenApi { get; set; }
    
    public required Dictionary<string, SwaggerOperation> Paths { get; init; }
    
    public  required Components Components { get; init; }
}

public class Components
{
    public required Dictionary<string, Schemas> Schemas { get; init; }
}

public class Schemas
{
    // ReSharper disable once InconsistentNaming
    public Dictionary<string, Properties>? properties { get; init; }
}

public class Properties
{
    public string Type { get; init; } = "nan";
}