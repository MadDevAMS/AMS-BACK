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
        }
    }
}
