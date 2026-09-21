namespace CapstonePM.Application.Status;

public sealed class SystemStatusService : ISystemStatusService
{
    private readonly IDatabaseProbe _databaseProbe;

    public SystemStatusService(IDatabaseProbe databaseProbe)
    {
        _databaseProbe = databaseProbe;
    }

    public async Task<SystemStatusResult> GetAsync(
        CancellationToken cancellationToken)
    {
        var databaseReachable =
            await _databaseProbe.CanConnectAsync(cancellationToken);

        return new SystemStatusResult(
            Api: "OK",
            Database: databaseReachable ? "OK" : "Unavailable");
    }
}