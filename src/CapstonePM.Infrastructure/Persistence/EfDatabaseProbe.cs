using CapstonePM.Application.Status;

namespace CapstonePM.Infrastructure.Persistence;

public sealed class EfDatabaseProbe : IDatabaseProbe
{
    private readonly CapstonePmDbContext _dbContext;

    public EfDatabaseProbe(CapstonePmDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> CanConnectAsync(
        CancellationToken cancellationToken)
    {
        return _dbContext.Database.CanConnectAsync(cancellationToken);
    }
}