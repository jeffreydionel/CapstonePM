using System.Net.Http.Json;
using CapstonePM.Api.Contracts;
using CapstonePM.Application.Status;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace CapstonePM.Api.Tests;

public sealed class StatusEndpointTests
{
    [Fact]
    public async Task GetStatus_ReturnsApiAndDatabaseStatus()
    {
        await using var factory =
            new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting(
                        "ConnectionStrings:CapstonePm",
                        "Server=localhost,1433;Database=CapstonePm;User Id=sa;Password=Your_Strong_Password123!;TrustServerCertificate=True");

                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll<IDatabaseProbe>();
                        services.AddSingleton<IDatabaseProbe>(new ReachableDatabaseProbe());
                    });
                });

        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/status");

        response.EnsureSuccessStatusCode();

        var payload = await response.Content.ReadFromJsonAsync<StatusResponse>();

        Assert.NotNull(payload);
        Assert.Equal("OK", payload.Api);
        Assert.Equal("OK", payload.Database);
    }

    private sealed class ReachableDatabaseProbe : IDatabaseProbe
    {
        public Task<bool> CanConnectAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}