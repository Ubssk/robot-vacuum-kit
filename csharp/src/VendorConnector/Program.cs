using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy())
    .AddCheck("postgres", () => HealthCheckResult.Healthy(), tags: new[] { "ready" })
    .AddCheck("redis", () => HealthCheckResult.Healthy(), tags: new[] { "ready" });

var app = builder.Build();

app.MapGet("/health/live", () => Results.Ok(new { status = "ok" }));
app.MapGet("/health/ready", async (HealthCheckService hc) =>
{
    var report = await hc.CheckHealthAsync(r => r.Tags.Contains("ready"));
    return report.Status == HealthStatus.Healthy
        ? Results.Ok(new { status = "ok" })
        : Results.StatusCode(503);
});
app.MapControllers();
app.UseHttpMetrics();
app.MapMetrics("/metrics");
app.Run();

public partial class Program { }
