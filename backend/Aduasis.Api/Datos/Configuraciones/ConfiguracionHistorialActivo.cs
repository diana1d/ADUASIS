using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aduasis.Api.Datos.Configuraciones;

public class ConfiguracionHistorialActivo : IEntityTypeConfiguration<HistorialActivo>
{
    public void Configure(EntityTypeBuilder<HistorialActivo> constructor)
    {
        constructor.ToTable("historial_activos");

        constructor.HasKey(h => h.Id);
        constructor.Property(h => h.Id).HasColumnName("id");
        constructor.Property(h => h.ActivoId).HasColumnName("activo_id").IsRequired();
        constructor.Property(h => h.UsuarioId).HasColumnName("usuario_id");
        constructor.Property(h => h.TipoEvento).HasColumnName("tipo_evento").HasMaxLength(50).IsRequired();
        constructor.Property(h => h.Descripcion).HasColumnName("descripcion").HasColumnType("text").IsRequired();
        constructor.Property(h => h.ValorAnterior).HasColumnName("valor_anterior").HasMaxLength(500);
        constructor.Property(h => h.ValorNuevo).HasColumnName("valor_nuevo").HasMaxLength(500);
        constructor.Property(h => h.EsNotaManual).HasColumnName("es_nota_manual").HasDefaultValue(false).IsRequired();
        constructor.Property(h => h.CreadoEn).HasColumnName("creado_en").HasDefaultValueSql("NOW()").IsRequired();

        constructor.HasIndex(h => h.ActivoId).HasDatabaseName("idx_historial_activos_activo_id");
        constructor.HasIndex(h => h.CreadoEn).HasDatabaseName("idx_historial_activos_creado_en");

        // Al eliminar el activo se elimina su historial
        constructor.HasOne(h => h.Activo)
            .WithMany()
            .HasForeignKey(h => h.ActivoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Al eliminar el usuario se mantiene el historial (SET NULL)
        constructor.HasOne(h => h.Usuario)
            .WithMany()
            .HasForeignKey(h => h.UsuarioId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
