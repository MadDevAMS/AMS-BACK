using AMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMS.Infrastructure.Persistence.Context.Configurations
{
    public class GroupUsersConfiguration : IEntityTypeConfiguration<GroupUsers>
    {
        public void Configure(EntityTypeBuilder<GroupUsers> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("GroupUsersId");

            builder.HasOne(g => g.Group)
                .WithMany(p => p.GroupUsers)
                .HasForeignKey(r => r.Groupid)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(r => r.User)
                .WithMany(p => p.GroupUsers)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
