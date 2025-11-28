using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class HealthEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly System.Net.Http.HttpClient _client;

    public HealthEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async System.Threading.Tasks.Task Liveness_ReturnsOk()
    {
        var resp = await _client.GetAsync("/health/live");
        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
    }

    [Fact]
    public async System.Threading.Tasks.Task Readiness_ReturnsOk()
    {
        var resp = await _client.GetAsync("/health/ready");
        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
    }
}
