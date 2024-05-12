using AMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMS.Infrastructure.Persistence.Context.Configurations
{
    public class RoleUserConfiguration : IEntityTypeConfiguration<RoleUser>
    {
        public void Configure(EntityTypeBuilder<RoleUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("RoleUserId");

            builder.HasOne(x => x.Role)
                .WithMany(p => p.RoleUsers)
                .HasForeignKey(r => r.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.User)
                .WithMany(p => p.RoleUsers)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
