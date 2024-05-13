using AMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMS.Infrastructure.Persistence.Context.Configurations
{
    public class EntidadConfigurations : IEntityTypeConfiguration<Entidad>
    {
        public void Configure(EntityTypeBuilder<Entidad> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("EntidadId");

            builder.Property(x => x.Nombre)
                .HasMaxLength(255);

            builder.Property(x => x.RazonSocial)
                .HasMaxLength(255);

            builder.Property(x => x.RUC)
                .HasMaxLength(255);

            builder.Property(x => x.Telefono)
                .HasMaxLength(255);

            builder.Property(x => x.Email)
                .HasMaxLength(255);

            builder.Property(x => x.Direccion)
                .HasMaxLength(255);
        }
    }
}
