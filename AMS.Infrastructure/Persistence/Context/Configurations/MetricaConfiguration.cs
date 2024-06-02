using AMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMS.Infrastructure.Persistence.Context.Configurations
{
    public class MetricaConfiguration : IEntityTypeConfiguration<Metrica>
    {
        public void Configure(EntityTypeBuilder<Metrica> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("MetricaId");

            builder.HasOne(x => x.PuntoMonitoreo)
                .WithMany(e => e.Metricas)
                .HasForeignKey(e => e.IdPuntoMonitoreo)
                .OnDelete(DeleteBehavior.ClientSetNull);


            builder.Property(x => x.Name)
                .HasMaxLength(255);
        }
    }
}
