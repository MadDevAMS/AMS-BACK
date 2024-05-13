using AMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMS.Infrastructure.Persistence.Context.Configurations
{
    public class GroupPermissionConfiguration : IEntityTypeConfiguration<GroupPermission>
    {
        public void Configure(EntityTypeBuilder<GroupPermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("RolePermissionId");

            builder.HasOne(g => g.Group)
                .WithMany(p => p.GroupPermission)
                .HasForeignKey(r => r.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(r => r.Permission)
                .WithMany(p => p.GroupPermission)
                .HasForeignKey(r => r.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
