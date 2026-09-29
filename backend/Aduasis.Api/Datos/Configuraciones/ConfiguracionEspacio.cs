using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aduasis.Api.Datos.Configuraciones;

public class ConfiguracionEspacio : IEntityTypeConfiguration<Espacio>
{
    public void Configure(EntityTypeBuilder<Espacio> constructor)
    {
        constructor.ToTable("espacios");

        constructor.HasKey(e => e.Id);
        constructor.Property(e => e.Id).HasColumnName("id");
        constructor.Property(e => e.PisoId).HasColumnName("piso_id").IsRequired();
        constructor.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
        constructor.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(200);
        constructor.Property(e => e.Activo).HasColumnName("activo").HasDefaultValue(true).IsRequired();
        constructor.Property(e => e.CreadoEn).HasColumnName("creado_en").HasDefaultValueSql("NOW()").IsRequired();

        constructor.HasIndex(e => new { e.PisoId, e.Nombre })
            .HasDatabaseName("idx_espacios_piso_nombre");

        constructor.HasOne(e => e.Piso)
            .WithMany(p => p.Espacios)
            .HasForeignKey(e => e.PisoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
