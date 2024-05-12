using AMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMS.Infrastructure.Persistence.Context.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("RolePermissionId");

            builder.HasOne(r => r.Role)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(r => r.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(r => r.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(r => r.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
