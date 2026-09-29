using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aduasis.Api.Datos.Configuraciones;

public class ConfiguracionEstadoActivo : IEntityTypeConfiguration<EstadoActivo>
{
    public void Configure(EntityTypeBuilder<EstadoActivo> constructor)
    {
        constructor.ToTable("estados_activo");

        constructor.HasKey(e => e.Id);
        constructor.Property(e => e.Id).HasColumnName("id");
        constructor.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
        constructor.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(200);
        constructor.Property(e => e.Activo).HasColumnName("activo").HasDefaultValue(true).IsRequired();
        constructor.Property(e => e.CreadoEn).HasColumnName("creado_en").HasDefaultValueSql("NOW()").IsRequired();

        constructor.HasIndex(e => e.Nombre).IsUnique().HasDatabaseName("idx_estados_activo_nombre");

        constructor.HasData(
            new EstadoActivo { Id = 1, Nombre = "Operativo",      Descripcion = "El activo funciona correctamente", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new EstadoActivo { Id = 2, Nombre = "En reparación",  Descripcion = "El activo está siendo reparado", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new EstadoActivo { Id = 3, Nombre = "En bodega",      Descripcion = "El activo está almacenado sin asignar", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new EstadoActivo { Id = 4, Nombre = "Dado de baja",   Descripcion = "El activo fue retirado del servicio", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new EstadoActivo { Id = 5, Nombre = "En préstamo",    Descripcion = "El activo está prestado temporalmente", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
