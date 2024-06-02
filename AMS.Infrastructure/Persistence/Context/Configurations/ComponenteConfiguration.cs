using AMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMS.Infrastructure.Persistence.Context.Configurations
{
    public class ComponenteConfiguration : IEntityTypeConfiguration<Componente>
    {
        public void Configure(EntityTypeBuilder<Componente> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("ComponenteId");

            builder.HasOne(u => u.Maquina)
                .WithMany(e => e.Componentes)
                .HasForeignKey(e => e.IdMaquina)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(x => x.Name)
                .HasMaxLength(150);

            builder.Property(x => x.Description)
                .HasMaxLength(255);
        }
    }
}
