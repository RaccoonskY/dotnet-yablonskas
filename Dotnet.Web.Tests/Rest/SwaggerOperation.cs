namespace Dotnet.Web.Tests.Rest;

public class SwaggerOperation
{
    // ReSharper disable once InconsistentNaming
    public Method? get { get; set; }
    
    // ReSharper disable once InconsistentNaming
    public Method? post { get; set; }
}

public class Method
{
    public Parameters[]? Parameters { get; set; }
}

public class Parameters
{
    public required  string Name { get; init; }
    
    public required Schema Schema { get; init; }
}

public class Schema
{
    public required string Type { get; init; }
    
    public string? Format { get; init; }
}