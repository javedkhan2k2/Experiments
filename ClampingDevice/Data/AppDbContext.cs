using ClampingDevice.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClampingDevice.Data;

// This class is the appdb context for the ClampingDevice application.
public class AppDbContext : DbContext
{
    public DbSet<ClampingData> ClampingsData { get; set; } = null!;
    public DbSet<Device> Devices { get; set; } = null!;
    public DbSet<EventLog> EventLogs { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Device>()
            .HasIndex(d => d.SerialNumber)
            .IsUnique();
    }
}
