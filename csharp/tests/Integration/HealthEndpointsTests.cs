using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class HealthEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HealthEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Ready_ReturnsOk()
    {
        var res = await _client.GetAsync("/health/ready");
        Assert.True(res.IsSuccessStatusCode);
    }

    [Fact]
    public async Task CanUpdate_ReturnsTrue()
    {
        var res = await _client.GetFromJsonAsync<dynamic>("/firmware/can-update?now=03:30&window=22:00-04:00");
        Assert.True((bool)res.canUpdate);
    }
}
