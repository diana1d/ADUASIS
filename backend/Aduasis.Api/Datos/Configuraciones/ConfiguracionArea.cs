using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aduasis.Api.Datos.Configuraciones;

public class ConfiguracionArea : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> constructor)
    {
        constructor.ToTable("areas");

        constructor.HasKey(a => a.Id);
        constructor.Property(a => a.Id).HasColumnName("id");
        constructor.Property(a => a.PisoId).HasColumnName("piso_id").IsRequired();
        constructor.Property(a => a.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
        constructor.Property(a => a.Descripcion).HasColumnName("descripcion").HasMaxLength(200);
        constructor.Property(a => a.Activo).HasColumnName("activo").HasDefaultValue(true).IsRequired();
        constructor.Property(a => a.CreadoEn).HasColumnName("creado_en").HasDefaultValueSql("NOW()").IsRequired();

        constructor.HasIndex(a => new { a.PisoId, a.Nombre })
            .HasDatabaseName("idx_areas_piso_nombre");

        constructor.HasOne(a => a.Piso)
            .WithMany(p => p.Areas)
            .HasForeignKey(a => a.PisoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
