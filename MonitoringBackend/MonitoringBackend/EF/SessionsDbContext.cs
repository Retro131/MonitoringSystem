using InfotecsBackend.EF.Configurations;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace InfotecsBackend.EF;

public class SessionsDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Device> Devices { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SessionConfiguration());
        modelBuilder.ApplyConfiguration(new DeviceConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}