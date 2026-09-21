namespace CapstonePM.Application.Status;

public interface ISystemStatusService
{
    Task<SystemStatusResult> GetAsync(
        CancellationToken cancellationToken);
}