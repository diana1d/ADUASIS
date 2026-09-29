using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aduasis.Api.Datos.Configuraciones;

/// <summary>
/// Configuración de EF Core para la entidad Rol.
/// Define el mapeo exacto a la tabla 'roles' en PostgreSQL.
/// </summary>
public class ConfiguracionRol : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> constructor)
    {
        constructor.ToTable("roles");

        constructor.HasKey(r => r.Id);
        constructor.Property(r => r.Id).HasColumnName("id");

        constructor.Property(r => r.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(50)
            .IsRequired();

        constructor.Property(r => r.Descripcion)
            .HasColumnName("descripcion")
            .HasMaxLength(200);

        constructor.Property(r => r.Activo)
            .HasColumnName("activo")
            .HasDefaultValue(true)
            .IsRequired();

        constructor.Property(r => r.CreadoEn)
            .HasColumnName("creado_en")
            .HasDefaultValueSql("NOW()")
            .IsRequired();

        constructor.HasIndex(r => r.Nombre)
            .IsUnique()
            .HasDatabaseName("idx_roles_nombre");

        // Seed de datos iniciales
        constructor.HasData(
            new Rol { Id = 1, Nombre = "Administrador", Descripcion = "Acceso total al sistema", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Rol { Id = 2, Nombre = "Funcionario",   Descripcion = "Gestión de activos y seguimiento de tickets", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Rol { Id = 3, Nombre = "Pasante",       Descripcion = "Acceso limitado a consultas y soporte básico", Activo = true, CreadoEn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
