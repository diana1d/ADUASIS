using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aduasis.Api.Datos.Configuraciones;

public class ConfiguracionActivo : IEntityTypeConfiguration<Activo>
{
    public void Configure(EntityTypeBuilder<Activo> constructor)
    {
        constructor.ToTable("activos");

        constructor.HasKey(a => a.Id);
        constructor.Property(a => a.Id).HasColumnName("id");

        // Identificación
        constructor.Property(a => a.CodigoActivo).HasColumnName("codigo_activo").HasMaxLength(50).IsRequired();
        constructor.Property(a => a.CodigoQr).HasColumnName("codigo_qr").HasMaxLength(100);

        // Descripción
        constructor.Property(a => a.NombreDescriptivo).HasColumnName("nombre_descriptivo").HasMaxLength(200).IsRequired();
        constructor.Property(a => a.Especificaciones).HasColumnName("especificaciones").HasColumnType("text");
        constructor.Property(a => a.Color).HasColumnName("color").HasMaxLength(50);

        // Clasificación
        constructor.Property(a => a.TipoActivoId).HasColumnName("tipo_activo_id").IsRequired();
        constructor.Property(a => a.MarcaId).HasColumnName("marca_id").IsRequired();
        constructor.Property(a => a.ModeloActivoId).HasColumnName("modelo_activo_id");

        // Estado
        constructor.Property(a => a.EstadoActivoId).HasColumnName("estado_activo_id").IsRequired();

        // Ubicación
        constructor.Property(a => a.EspacioId).HasColumnName("espacio_id");
        constructor.Property(a => a.AreaId).HasColumnName("area_id");

        // Asignación
        constructor.Property(a => a.UsuarioAsignadoId).HasColumnName("usuario_asignado_id");

        // Red
        constructor.Property(a => a.DireccionIp).HasColumnName("direccion_ip").HasMaxLength(45);

        // Administrativo
        constructor.Property(a => a.FechaAdquisicion).HasColumnName("fecha_adquisicion");
        constructor.Property(a => a.Observaciones).HasColumnName("observaciones").HasColumnType("text");

        // Auditoría
        constructor.Property(a => a.CreadoEn).HasColumnName("creado_en").HasDefaultValueSql("NOW()").IsRequired();
        constructor.Property(a => a.ActualizadoEn).HasColumnName("actualizado_en").HasDefaultValueSql("NOW()").IsRequired();
        constructor.Property(a => a.CreadoPorId).HasColumnName("creado_por_id");

        // Índices
        constructor.HasIndex(a => a.CodigoActivo).IsUnique().HasDatabaseName("idx_activos_codigo_activo");
        constructor.HasIndex(a => a.CodigoQr).IsUnique().HasDatabaseName("idx_activos_codigo_qr").HasFilter("codigo_qr IS NOT NULL");
        constructor.HasIndex(a => a.EstadoActivoId).HasDatabaseName("idx_activos_estado");
        constructor.HasIndex(a => a.EspacioId).HasDatabaseName("idx_activos_espacio");
        constructor.HasIndex(a => a.AreaId).HasDatabaseName("idx_activos_area");

        // Relaciones
        constructor.HasOne(a => a.TipoActivo)
            .WithMany(t => t.Activos)
            .HasForeignKey(a => a.TipoActivoId)
            .OnDelete(DeleteBehavior.Restrict);

        constructor.HasOne(a => a.Marca)
            .WithMany(m => m.Activos)
            .HasForeignKey(a => a.MarcaId)
            .OnDelete(DeleteBehavior.Restrict);

        constructor.HasOne(a => a.ModeloActivo)
            .WithMany(m => m.Activos)
            .HasForeignKey(a => a.ModeloActivoId)
            .OnDelete(DeleteBehavior.Restrict);

        constructor.HasOne(a => a.EstadoActivo)
            .WithMany(e => e.Activos)
            .HasForeignKey(a => a.EstadoActivoId)
            .OnDelete(DeleteBehavior.Restrict);

        constructor.HasOne(a => a.Espacio)
            .WithMany(e => e.Activos)
            .HasForeignKey(a => a.EspacioId)
            .OnDelete(DeleteBehavior.SetNull);

        constructor.HasOne(a => a.Area)
            .WithMany(ar => ar.Activos)
            .HasForeignKey(a => a.AreaId)
            .OnDelete(DeleteBehavior.SetNull);

        // Dos relaciones al mismo usuario — necesitan nombrar las FK explícitamente
        constructor.HasOne(a => a.UsuarioAsignado)
            .WithMany()
            .HasForeignKey(a => a.UsuarioAsignadoId)
            .OnDelete(DeleteBehavior.SetNull);

        constructor.HasOne(a => a.CreadoPor)
            .WithMany()
            .HasForeignKey(a => a.CreadoPorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
