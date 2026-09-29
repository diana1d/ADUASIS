using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aduasis.Api.Datos.Configuraciones;

public class ConfiguracionPiso : IEntityTypeConfiguration<Piso>
{
    public void Configure(EntityTypeBuilder<Piso> constructor)
    {
        constructor.ToTable("pisos");

        constructor.HasKey(p => p.Id);
        constructor.Property(p => p.Id).HasColumnName("id");
        constructor.Property(p => p.EdificioId).HasColumnName("edificio_id").IsRequired();
        constructor.Property(p => p.Nombre).HasColumnName("nombre").HasMaxLength(50).IsRequired();
        constructor.Property(p => p.Orden).HasColumnName("orden").HasDefaultValue(0).IsRequired();
        constructor.Property(p => p.Activo).HasColumnName("activo").HasDefaultValue(true).IsRequired();
        constructor.Property(p => p.CreadoEn).HasColumnName("creado_en").HasDefaultValueSql("NOW()").IsRequired();

        constructor.HasIndex(p => new { p.EdificioId, p.Nombre })
            .IsUnique()
            .HasDatabaseName("idx_pisos_edificio_nombre");

        constructor.HasOne(p => p.Edificio)
            .WithMany(e => e.Pisos)
            .HasForeignKey(p => p.EdificioId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed: PB, P1 y Exterior del edificio principal
        constructor.HasData(
            new Piso { Id = 1, EdificioId = 1, Nombre = "Planta Baja", Orden = 0, Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Piso { Id = 2, EdificioId = 1, Nombre = "Piso 1",      Orden = 1, Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Piso { Id = 3, EdificioId = 1, Nombre = "Exterior",    Orden = 99, Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
