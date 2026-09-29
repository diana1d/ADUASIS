using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aduasis.Api.Datos.Configuraciones;

public class ConfiguracionMarca : IEntityTypeConfiguration<Marca>
{
    public void Configure(EntityTypeBuilder<Marca> constructor)
    {
        constructor.ToTable("marcas");

        constructor.HasKey(m => m.Id);
        constructor.Property(m => m.Id).HasColumnName("id");
        constructor.Property(m => m.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
        constructor.Property(m => m.Activo).HasColumnName("activo").HasDefaultValue(true).IsRequired();
        constructor.Property(m => m.CreadoEn).HasColumnName("creado_en").HasDefaultValueSql("NOW()").IsRequired();

        constructor.HasIndex(m => m.Nombre).IsUnique().HasDatabaseName("idx_marcas_nombre");

        // Seed de marcas comunes en la institución
        constructor.HasData(
            new Marca { Id = 1,  Nombre = "Dell",       Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Marca { Id = 2,  Nombre = "HP",         Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Marca { Id = 3,  Nombre = "Cisco",      Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Marca { Id = 4,  Nombre = "Lenovo",     Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Marca { Id = 5,  Nombre = "Epson",      Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Marca { Id = 6,  Nombre = "Samsung",    Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Marca { Id = 7,  Nombre = "Acer",       Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Marca { Id = 8,  Nombre = "Asus",       Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Marca { Id = 9,  Nombre = "APC",        Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Marca { Id = 10, Nombre = "Huawei",     Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Marca { Id = 11, Nombre = "TP-Link",    Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Marca { Id = 12, Nombre = "Otra",       Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
