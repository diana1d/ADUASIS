using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aduasis.Api.Datos.Configuraciones;

public class ConfiguracionAsignacion : IEntityTypeConfiguration<Asignacion>
{
    public void Configure(EntityTypeBuilder<Asignacion> constructor)
    {
        constructor.ToTable("asignaciones");

        constructor.HasKey(a => a.Id);
        constructor.Property(a => a.Id).HasColumnName("id");
        constructor.Property(a => a.ActivoId).HasColumnName("activo_id").IsRequired();
        constructor.Property(a => a.UsuarioId).HasColumnName("usuario_id");
        constructor.Property(a => a.FechaInicio).HasColumnName("fecha_inicio").HasDefaultValueSql("NOW()").IsRequired();
        constructor.Property(a => a.FechaFin).HasColumnName("fecha_fin");
        constructor.Property(a => a.Observaciones).HasColumnName("observaciones").HasColumnType("text");
        constructor.Property(a => a.RegistradoPorId).HasColumnName("registrado_por_id");
        constructor.Property(a => a.CreadoEn).HasColumnName("creado_en").HasDefaultValueSql("NOW()").IsRequired();

        constructor.HasIndex(a => a.ActivoId).HasDatabaseName("idx_asignaciones_activo_id");
        // Índice parcial: buscar la asignación activa (fecha_fin IS NULL) es frecuente
        constructor.HasIndex(a => new { a.ActivoId, a.FechaFin })
            .HasDatabaseName("idx_asignaciones_activo_activa");

        constructor.HasOne(a => a.Activo)
            .WithMany()
            .HasForeignKey(a => a.ActivoId)
            .OnDelete(DeleteBehavior.Cascade);

        constructor.HasOne(a => a.Usuario)
            .WithMany()
            .HasForeignKey(a => a.UsuarioId)
            .OnDelete(DeleteBehavior.SetNull);

        constructor.HasOne(a => a.RegistradoPor)
            .WithMany()
            .HasForeignKey(a => a.RegistradoPorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
