using System.Net.Http.Json;
using System.Text.Json;
using Dotnet.Web.Attributes;
using Dotnet.Web.Models;
using Dotnet.Web.Tests.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Dotnet.Web.Tests.Rest;

public class SwaggerTests : WebApplicationFactoryTestsBase
{
    public SwaggerTests(DotnetWebApplicationFactory factory) : base(factory) { }

    private static SwaggerDocument DeserializeSwaggerDocument()
    {
        using var stream = new FileStream("Rest/swagger.json", FileMode.Open);
        return JsonSerializer.Deserialize<SwaggerDocument>(stream, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException("swagger.json is missing");
    }

    private async Task<SwaggerDocument> GetStudentSwaggerAsync()
    {
        var response = await HttpClient.GetAsync("/swagger/v1/swagger.json");
        var document = await response.Content.ReadFromJsonAsync<SwaggerDocument>();
        Assert.NotNull(document);
        return document ?? throw new InvalidOperationException();
    }

    [Homework(Homeworks.Rest)]
    public async Task SwaggerJson_ReturnsCorrectSwaggerDefinition()
    {
        var currentSwaggerDocument = DeserializeSwaggerDocument();
        var content = await GetStudentSwaggerAsync();

        content.Should().NotBeNull();
        content.Paths.Should().NotBeEmpty();
        currentSwaggerDocument.Paths.Keys
            .Should()
            .AllSatisfy(s => content.Paths.Keys.Should().Contain(s));
    }

    [Homework(Homeworks.Rest)]
    public async Task SwaggerJson_ReturnsCorrectSwaggerDto()
    {
        var currentSwaggerDocument = DeserializeSwaggerDocument();
        var content = await GetStudentSwaggerAsync();

        Assert.All(currentSwaggerDocument.Components.Schemas.Keys,
            s => Assert.Contains(s, content.Components.Schemas.Keys));
    }

    [Homework(Homeworks.Rest)]
    public async Task SwaggerJson_ReturnsCorrectSwaggerDtoType()
    {
        var currentSwaggerDocument = DeserializeSwaggerDocument();
        var content = await GetStudentSwaggerAsync();

        var currentSwaggerDto = currentSwaggerDocument.Components.Schemas
            .Where(x => x.Value.properties != null)
            .ToDictionary(
                x => x.Key,
                x => x.Value);

        var contentDto = content.Components.Schemas
            .Where(x => x.Value.properties != null)
            .ToDictionary(
                x => x.Key,
                x => x.Value);

        Assert.All(currentSwaggerDto, s => Assert.All(
            contentDto[s.Key].properties!,
            x => Assert.Equal(x.Value.ToString(), s.Value.properties![x.Key].ToString())));
    }

    [Homework(Homeworks.Rest)]
    public async Task SwaggerJson_ReturnsCorrectSwaggerDefinitionProperties()
    {
        var currentSwaggerDocument = DeserializeSwaggerDocument();
        var content = await GetStudentSwaggerAsync();

        var docParam = currentSwaggerDocument.Paths.ToDictionary(
                x => x.Key,
                x => x.Value.post ?? x.Value.get)
            .Where(x => x.Value?.Parameters != null)
            .ToDictionary(x => x.Key, x => x.Value);

        var contentParam = content.Paths.ToDictionary(
                x => x.Key,
                x => x.Value.post ?? x.Value.get)
            .Where(x => x.Value?.Parameters != null)
            .ToList()
            .ToDictionary(x => x.Key, x => x.Value);

        Assert.NotNull(contentParam);
        Assert.All(contentParam, s => Assert.Equal(
            docParam[s.Key]?.Parameters?.First().Name,
            (s.Value?.Parameters ?? throw new InvalidOperationException()).First().Name));
    }
}