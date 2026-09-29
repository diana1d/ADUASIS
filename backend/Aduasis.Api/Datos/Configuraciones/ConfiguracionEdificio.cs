using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aduasis.Api.Datos.Configuraciones;

public class ConfiguracionEdificio : IEntityTypeConfiguration<Edificio>
{
    public void Configure(EntityTypeBuilder<Edificio> constructor)
    {
        constructor.ToTable("edificios");

        constructor.HasKey(e => e.Id);
        constructor.Property(e => e.Id).HasColumnName("id");
        constructor.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
        constructor.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(200);
        constructor.Property(e => e.Activo).HasColumnName("activo").HasDefaultValue(true).IsRequired();
        constructor.Property(e => e.CreadoEn).HasColumnName("creado_en").HasDefaultValueSql("NOW()").IsRequired();

        constructor.HasIndex(e => e.Nombre).IsUnique().HasDatabaseName("idx_edificios_nombre");

        constructor.HasData(
            new Edificio { Id = 1, Nombre = "Aduana Nacional - Gerencia Regional La Paz", Descripcion = "Edificio principal", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
