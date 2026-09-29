using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Aduasis.Api.Datos;

/// <summary>
/// Contexto principal de Entity Framework Core para ADUASIS.
/// Las configuraciones de cada entidad están en Datos/Configuraciones/
/// y se aplican automáticamente mediante ApplyConfigurationsFromAssembly.
/// </summary>
public class ContextoAduasis : DbContext
{
    public ContextoAduasis(DbContextOptions<ContextoAduasis> opciones)
        : base(opciones)
    {
    }

    // ── Sprint 1: Autenticación ──────────────────────
    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<TokenRefresco> TokensRefresco => Set<TokenRefresco>();

    // ── Sprint 2: Catálogos ──────────────────────────
    public DbSet<TipoActivo> TiposActivo => Set<TipoActivo>();
    public DbSet<Marca> Marcas => Set<Marca>();
    public DbSet<ModeloActivo> ModelosActivo => Set<ModeloActivo>();
    public DbSet<EstadoActivo> EstadosActivo => Set<EstadoActivo>();

    // ── Sprint 2: Ubicación ──────────────────────────
    public DbSet<Edificio> Edificios => Set<Edificio>();
    public DbSet<Piso> Pisos => Set<Piso>();
    public DbSet<Area> Areas => Set<Area>();
    public DbSet<Espacio> Espacios => Set<Espacio>();

    // ── Sprint 2: Activos ────────────────────────────
    public DbSet<Activo> Activos => Set<Activo>();

    // ── Sprint 3: Historial y Asignaciones ───────────
    public DbSet<HistorialActivo> HistorialActivos => Set<HistorialActivo>();
    public DbSet<Asignacion> Asignaciones => Set<Asignacion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplica automáticamente todas las clases IEntityTypeConfiguration<T>
        // que estén en la carpeta Datos/Configuraciones/
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextoAduasis).Assembly);
    }
}
