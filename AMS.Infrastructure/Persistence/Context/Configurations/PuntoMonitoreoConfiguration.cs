using AMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMS.Infrastructure.Persistence.Context.Configurations
{
    public class PuntoMonitoreoConfiguration : IEntityTypeConfiguration<PuntoMonitoreo>
    {
        public void Configure(EntityTypeBuilder<PuntoMonitoreo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("PuntoMonitoreoId");

            builder.HasOne(x => x.Componente)
                .WithMany(e => e.PuntosMonitoreo)
                .HasForeignKey(e => e.IdComponente)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(p => p.DireccionMedicion)
                .HasMaxLength(255);

            builder.Property(p => p.AnguloDireccion)
                .HasMaxLength(255);
        }
    }
}
