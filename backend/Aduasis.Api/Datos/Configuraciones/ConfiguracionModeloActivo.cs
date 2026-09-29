using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aduasis.Api.Datos.Configuraciones;

public class ConfiguracionModeloActivo : IEntityTypeConfiguration<ModeloActivo>
{
    public void Configure(EntityTypeBuilder<ModeloActivo> constructor)
    {
        constructor.ToTable("modelos_activo");

        constructor.HasKey(m => m.Id);
        constructor.Property(m => m.Id).HasColumnName("id");
        constructor.Property(m => m.MarcaId).HasColumnName("marca_id").IsRequired();
        constructor.Property(m => m.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
        constructor.Property(m => m.Activo).HasColumnName("activo").HasDefaultValue(true).IsRequired();
        constructor.Property(m => m.CreadoEn).HasColumnName("creado_en").HasDefaultValueSql("NOW()").IsRequired();

        // Un modelo es único dentro de una marca
        constructor.HasIndex(m => new { m.MarcaId, m.Nombre })
            .IsUnique()
            .HasDatabaseName("idx_modelos_activo_marca_nombre");

        constructor.HasOne(m => m.Marca)
            .WithMany(ma => ma.Modelos)
            .HasForeignKey(m => m.MarcaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed de modelos más comunes (basados en las etiquetas de ejemplo)
        constructor.HasData(
            // Dell
            new ModeloActivo { Id = 1, MarcaId = 1, Nombre = "OptiPlex 7070",      Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new ModeloActivo { Id = 2, MarcaId = 1, Nombre = "OptiPlex 3080",      Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new ModeloActivo { Id = 3, MarcaId = 1, Nombre = "Latitude 5420",      Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            // HP
            new ModeloActivo { Id = 4, MarcaId = 2, Nombre = "ProDesk 400 G7",     Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new ModeloActivo { Id = 5, MarcaId = 2, Nombre = "LaserJet Enterprise", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new ModeloActivo { Id = 6, MarcaId = 2, Nombre = "EliteBook 840",      Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
