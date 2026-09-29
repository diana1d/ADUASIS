using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aduasis.Api.Datos.Configuraciones;

/// <summary>
/// Configuración de EF Core para la entidad TokenRefresco.
/// ON DELETE CASCADE: al eliminar un usuario, sus tokens se eliminan.
/// </summary>
public class ConfiguracionTokenRefresco : IEntityTypeConfiguration<TokenRefresco>
{
    public void Configure(EntityTypeBuilder<TokenRefresco> constructor)
    {
        constructor.ToTable("tokens_refresco");

        constructor.HasKey(t => t.Id);
        constructor.Property(t => t.Id).HasColumnName("id");

        constructor.Property(t => t.UsuarioId)
            .HasColumnName("usuario_id")
            .IsRequired();

        constructor.Property(t => t.Token)
            .HasColumnName("token")
            .HasMaxLength(500)
            .IsRequired();

        constructor.Property(t => t.ExpiraEn)
            .HasColumnName("expira_en")
            .IsRequired();

        constructor.Property(t => t.Usado)
            .HasColumnName("usado")
            .HasDefaultValue(false)
            .IsRequired();

        constructor.Property(t => t.Revocado)
            .HasColumnName("revocado")
            .HasDefaultValue(false)
            .IsRequired();

        constructor.Property(t => t.CreadoEn)
            .HasColumnName("creado_en")
            .HasDefaultValueSql("NOW()")
            .IsRequired();

        constructor.Property(t => t.IpOrigen)
            .HasColumnName("ip_origen")
            .HasMaxLength(45);

        constructor.HasIndex(t => t.Token)
            .IsUnique()
            .HasDatabaseName("idx_tokens_refresco_token");

        constructor.HasIndex(t => t.UsuarioId)
            .HasDatabaseName("idx_tokens_refresco_usuario_id");

        // Relación con Usuario — CASCADE al eliminar usuario
        constructor.HasOne(t => t.Usuario)
            .WithMany(u => u.TokensRefresco)
            .HasForeignKey(t => t.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
