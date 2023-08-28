
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Dotnet.Intro.Web.Tests
{
    public class IntroControllerTests
    {
  
        [Theory]
        [InlineData("/calculator/add?x=2&y=3")]
        [InlineData("/calculator/mul?x=4&y=5")]
        [InlineData("/calculator/sub?x=8&y=6")]
        [InlineData("/calculator/div?x=12&y=3")]
        public async Task Get_CalculatorActions_ReturnsExpectedResult(string url)
        {
            var _factory = new WebApplicationFactory<Program>();
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await response.Content.ReadAsStringAsync();
        }
    }
}
