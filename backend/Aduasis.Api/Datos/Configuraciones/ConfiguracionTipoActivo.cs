using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aduasis.Api.Datos.Configuraciones;

public class ConfiguracionTipoActivo : IEntityTypeConfiguration<TipoActivo>
{
    public void Configure(EntityTypeBuilder<TipoActivo> constructor)
    {
        constructor.ToTable("tipos_activo");

        constructor.HasKey(t => t.Id);
        constructor.Property(t => t.Id).HasColumnName("id");
        constructor.Property(t => t.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
        constructor.Property(t => t.Descripcion).HasColumnName("descripcion").HasMaxLength(200);
        constructor.Property(t => t.Activo).HasColumnName("activo").HasDefaultValue(true).IsRequired();
        constructor.Property(t => t.CreadoEn).HasColumnName("creado_en").HasDefaultValueSql("NOW()").IsRequired();

        constructor.HasIndex(t => t.Nombre).IsUnique().HasDatabaseName("idx_tipos_activo_nombre");

        // Seed inicial
        constructor.HasData(
            new TipoActivo { Id = 1, Nombre = "Equipo de computación",  Descripcion = "Computadoras de escritorio, laptops y similares", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new TipoActivo { Id = 2, Nombre = "Impresora",              Descripcion = "Impresoras láser, de inyección y multifuncionales", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new TipoActivo { Id = 3, Nombre = "Switch",                 Descripcion = "Switches de red administrables y no administrables", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new TipoActivo { Id = 4, Nombre = "Router",                 Descripcion = "Routers y equipos de enrutamiento", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new TipoActivo { Id = 5, Nombre = "UPS",                    Descripcion = "Sistemas de alimentación ininterrumpida", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new TipoActivo { Id = 6, Nombre = "Monitor",                Descripcion = "Monitores y pantallas", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new TipoActivo { Id = 7, Nombre = "Teléfono IP",            Descripcion = "Teléfonos y dispositivos de telefonía IP", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new TipoActivo { Id = 8, Nombre = "Escáner",                Descripcion = "Escáneres documentales y de código de barras", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new TipoActivo { Id = 9, Nombre = "Servidor",               Descripcion = "Servidores físicos y de rack", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new TipoActivo { Id = 10, Nombre = "Otro",                  Descripcion = "Otros equipos tecnológicos", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
