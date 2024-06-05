using AMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMS.Infrastructure.Persistence.Context.Configurations
{
    public class MaquinaConfiguration : IEntityTypeConfiguration<Maquina>
    {
        public void Configure(EntityTypeBuilder<Maquina> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("MaquinaId");

            builder.HasOne(u => u.Area)
                .WithMany(e => e.Maquinas)
                .HasForeignKey(e => e.IdArea)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(x => x.Name)
                .HasMaxLength(150);

            builder.Property(x => x.Description)
                .HasMaxLength(255);

            builder.Property(x => x.TipoMaquina)
                .HasMaxLength(150);
        }
    }
}
