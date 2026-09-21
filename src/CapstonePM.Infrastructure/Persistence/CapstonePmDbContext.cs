using Microsoft.EntityFrameworkCore;

namespace CapstonePM.Infrastructure.Persistence;

public sealed class CapstonePmDbContext : DbContext
{
    public CapstonePmDbContext(
        DbContextOptions<CapstonePmDbContext> options)
        : base(options)
    {
    }
}