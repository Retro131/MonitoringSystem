using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace InfotecsBackend.EF.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.LastSeenAt)
            .IsRequired();

        builder.HasIndex(x => x.LastSeenAt)
            .HasDatabaseName("IX_Device_LastSeenAt");
        
        builder.HasMany(x => x.Sessions)
            .WithOne(x => x.Device)
            .HasForeignKey(x => x.DeviceId);
    }
}