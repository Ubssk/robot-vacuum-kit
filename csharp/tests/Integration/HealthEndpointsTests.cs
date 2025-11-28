using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

public class HealthEndpointsTests : Xunit.IClassFixture<WebApplicationFactory<Program>>
{
    private readonly System.Net.Http.HttpClient _client;

    public HealthEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Xunit.Fact]
    public async System.Threading.Tasks.Task Liveness_ReturnsOk()
    {
        var resp = await _client.GetAsync("/health/live");
        Xunit.Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
    }

    [Xunit.Fact]
    public async System.Threading.Tasks.Task Readiness_ReturnsOk()
    {
        var resp = await _client.GetAsync("/health/ready");
        Xunit.Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
    }
}
