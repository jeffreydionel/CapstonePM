using CapstonePM.Api.Contracts;
using CapstonePM.Application.Status;
using CapstonePM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString =
            builder.Configuration.GetConnectionString("CapstonePm");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'CapstonePm' is not configured.");
        }

        builder.Services.AddDbContext<CapstonePmDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddScoped<IDatabaseProbe, EfDatabaseProbe>();
        builder.Services.AddScoped<ISystemStatusService, SystemStatusService>();

        var app = builder.Build();

        app.MapGet(
            "/api/status",
            async (
                ISystemStatusService statusService,
                CancellationToken cancellationToken) =>
            {
                var status = await statusService.GetAsync(cancellationToken);

                return Results.Ok(new StatusResponse(status.Api, status.Database));
            });

        app.Run();
    }
}