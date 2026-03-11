using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace InfotecsBackend.EF.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.Version)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.StartTime).IsRequired();
        builder.Property(x => x.EndTime).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasIndex(x => x.DeviceId)
            .HasDatabaseName("IX_Session_DeviceId");
        
        builder.HasIndex(x => new { x.DeviceId, x.StartTime })
            .HasDatabaseName("IX_Session_DeviceId_StartTime");

        builder.HasIndex(x => x.IsDeleted)
            .HasFilter("\"IsDeleted\" = false");
        
        builder.HasIndex(x => x.EndTime)
            .HasDatabaseName("IX_Session_EndTime");
        
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasOne(x => x.Device)
            .WithMany(x => x.Sessions)
            .HasForeignKey(x => x.DeviceId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}