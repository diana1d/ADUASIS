using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aduasis.Api.Datos.Configuraciones;

/// <summary>
/// Configuración de EF Core para la entidad Usuario.
/// Define el mapeo a la tabla 'usuarios' y sus restricciones.
/// El seed del administrador inicial se maneja en el Inicializador
/// porque requiere BCrypt en tiempo de ejecución.
/// </summary>
public class ConfiguracionUsuario : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> constructor)
    {
        constructor.ToTable("usuarios");

        constructor.HasKey(u => u.Id);
        constructor.Property(u => u.Id).HasColumnName("id");

        constructor.Property(u => u.RolId)
            .HasColumnName("rol_id")
            .IsRequired();

        constructor.Property(u => u.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        constructor.Property(u => u.Apellido)
            .HasColumnName("apellido")
            .HasMaxLength(100)
            .IsRequired();

        constructor.Property(u => u.Correo)
            .HasColumnName("correo")
            .HasMaxLength(150)
            .IsRequired();

        constructor.Property(u => u.NombreUsuario)
            .HasColumnName("nombre_usuario")
            .HasMaxLength(50)
            .IsRequired();

        constructor.Property(u => u.HashContrasena)
            .HasColumnName("hash_contrasena")
            .HasMaxLength(255)
            .IsRequired();

        constructor.Property(u => u.Activo)
            .HasColumnName("activo")
            .HasDefaultValue(true)
            .IsRequired();

        constructor.Property(u => u.UltimoAcceso)
            .HasColumnName("ultimo_acceso");

        constructor.Property(u => u.CreadoEn)
            .HasColumnName("creado_en")
            .HasDefaultValueSql("NOW()")
            .IsRequired();

        constructor.Property(u => u.ActualizadoEn)
            .HasColumnName("actualizado_en")
            .HasDefaultValueSql("NOW()")
            .IsRequired();

        // Restricciones de unicidad
        constructor.HasIndex(u => u.Correo)
            .IsUnique()
            .HasDatabaseName("idx_usuarios_correo");

        constructor.HasIndex(u => u.NombreUsuario)
            .IsUnique()
            .HasDatabaseName("idx_usuarios_nombre_usuario");

        constructor.HasIndex(u => u.RolId)
            .HasDatabaseName("idx_usuarios_rol_id");

        // Relación con Rol
        constructor.HasOne(u => u.Rol)
            .WithMany(r => r.Usuarios)
            .HasForeignKey(u => u.RolId)
            .OnDelete(DeleteBehavior.Restrict); // No eliminar rol si tiene usuarios
    }
}
