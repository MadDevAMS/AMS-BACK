﻿using AMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMS.Infrastructure.Persistence.Context.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("GroupId");

            builder.HasMany(g => g.GroupUsers)
                .WithOne(gu => gu.Group)
                .HasForeignKey(gu => gu.GroupId);

            builder.HasOne(u => u.Entidad)
                .WithMany(e => e.Groups)
                .HasForeignKey(e => e.IdEntidad)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(x => x.Name)
                .HasMaxLength(150);
        }
    }
}
