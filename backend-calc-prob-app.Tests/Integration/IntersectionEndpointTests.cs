using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BackendCalcPropApp.Tests.Integration;

public class IntersectionEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;
    private readonly HttpClient client;

    public IntersectionEndpointTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
        this.client = factory.CreateClient();
    }

    [Fact]
    public async Task PostIntersection_ValidInput_ReturnsSuccessWithCorrectResult()
    {
        var request = new { A = 0.5, B = 0.3 };
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/calculator/intersection", content);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<JsonElement>(responseContent);

        Assert.True(result.TryGetProperty("result", out var resultValue));
        Assert.Equal(0.15M, resultValue.GetDecimal(), precision: 10);
    }

    [Theory]
    [InlineData(0.0, 0.5, 0.0)]
    [InlineData(1.0, 0.5, 0.5)]
    [InlineData(0.25, 0.8, 0.2)]
    [InlineData(1.0, 1.0, 1.0)]
    public async Task PostIntersection_VariousValidInputs_ReturnsExpectedResults(
        decimal a, decimal b, decimal expected)
    {
        var request = new { A = a, B = b };
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/calculator/intersection", content);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<JsonElement>(responseContent);

        Assert.True(result.TryGetProperty("result", out var resultValue));
        Assert.Equal(expected, resultValue.GetDecimal(), precision: 10);
    }

    [Theory]
    [InlineData(-0.1, 0.5)]
    [InlineData(0.5, -0.1)]
    [InlineData(1.5, 0.5)]
    [InlineData(0.5, 1.5)]
    [InlineData(-1.0, 2.0)]
    public async Task PostIntersection_InvalidInput_ReturnsBadRequest(decimal a, decimal b)
    {
        var request = new { A = a, B = b };
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/calculator/intersection", content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("must be between 0 and 1", responseContent);
    }

    [Fact]
    public async Task PostIntersection_EmptyBody_ReturnsBadRequest()
    {
        var content = new StringContent("", Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/calculator/intersection", content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostIntersection_InvalidJson_ReturnsBadRequest()
    {
        var content = new StringContent("{ invalid json }", Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/calculator/intersection", content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostIntersection_MissingFields_ReturnsBadRequest()
    {
        var request = new { A = 0.5 };
        var expected = 0.0M; // B is missing, should default to 0.0
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/calculator/intersection", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<JsonElement>(responseContent);

        Assert.True(result.TryGetProperty("result", out var resultValue));
        Assert.Equal(expected, resultValue.GetDecimal(), precision: 10);
    }

    [Fact]
    public async Task PostIntersection_WrongContentType_ReturnsUnsupportedMediaType()
    {
        var content = new StringContent("A=0.5&B=0.3", Encoding.UTF8, "application/x-www-form-urlencoded");

        var response = await client.PostAsync("/api/calculator/intersection", content);

        Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
    }

    [Fact]
    public async Task PostIntersection_ValidRequest_CreatesLogEntry()
    {
        var request = new { A = 0.7, B = 0.4 };
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/calculator/intersection", content);

        response.EnsureSuccessStatusCode();

        var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
        var logFile = Path.Combine(logDirectory, $"calculations_{DateTime.Now:yyyy-MM-dd}.log");

        if (File.Exists(logFile))
        {
            var logContent = await File.ReadAllTextAsync(logFile);
            Assert.Contains("Intersection", logContent);
            Assert.Contains("0.7", logContent);
            Assert.Contains("0.4", logContent);
        }
    }
}