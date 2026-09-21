namespace CapstonePM.Application.Status;

public interface IDatabaseProbe
{
    Task<bool> CanConnectAsync(CancellationToken cancellationToken);
}