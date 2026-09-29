using Aduasis.Api.Datos;
using Aduasis.Api.Interfaces;
using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Aduasis.Api.Repositorios;

public class RepositorioCatalogos : IRepositorioCatalogos
{
    private readonly ContextoAduasis _contexto;

    public RepositorioCatalogos(ContextoAduasis contexto)
    {
        _contexto = contexto;
    }

    // ── Tipos de activo ──────────────────────────────
    public async Task<IEnumerable<TipoActivo>> ObtenerTiposActivoAsync() =>
        await _contexto.TiposActivo.Where(t => t.Activo).OrderBy(t => t.Nombre).AsNoTracking().ToListAsync();

    public async Task<TipoActivo?> ObtenerTipoActivoPorIdAsync(int id) =>
        await _contexto.TiposActivo.FindAsync(id);

    public async Task<TipoActivo> AgregarTipoActivoAsync(TipoActivo tipo)
    {
        await _contexto.TiposActivo.AddAsync(tipo);
        await _contexto.SaveChangesAsync();
        return tipo;
    }

    public async Task ActualizarTipoActivoAsync(TipoActivo tipo)
    {
        _contexto.TiposActivo.Update(tipo);
        await _contexto.SaveChangesAsync();
    }

    // ── Marcas ───────────────────────────────────────
    public async Task<IEnumerable<Marca>> ObtenerMarcasAsync() =>
        await _contexto.Marcas.Where(m => m.Activo).OrderBy(m => m.Nombre).AsNoTracking().ToListAsync();

    public async Task<Marca?> ObtenerMarcaPorIdAsync(int id) =>
        await _contexto.Marcas.FindAsync(id);

    public async Task<Marca> AgregarMarcaAsync(Marca marca)
    {
        await _contexto.Marcas.AddAsync(marca);
        await _contexto.SaveChangesAsync();
        return marca;
    }

    public async Task ActualizarMarcaAsync(Marca marca)
    {
        _contexto.Marcas.Update(marca);
        await _contexto.SaveChangesAsync();
    }

    // ── Modelos ──────────────────────────────────────
    public async Task<IEnumerable<ModeloActivo>> ObtenerModelosAsync(int? marcaId = null)
    {
        var consulta = _contexto.ModelosActivo
            .Include(m => m.Marca)
            .Where(m => m.Activo)
            .AsNoTracking();

        if (marcaId.HasValue)
            consulta = consulta.Where(m => m.MarcaId == marcaId.Value);

        return await consulta.OrderBy(m => m.Nombre).ToListAsync();
    }

    public async Task<ModeloActivo?> ObtenerModeloPorIdAsync(int id) =>
        await _contexto.ModelosActivo.Include(m => m.Marca).FirstOrDefaultAsync(m => m.Id == id);

    public async Task<ModeloActivo> AgregarModeloAsync(ModeloActivo modelo)
    {
        await _contexto.ModelosActivo.AddAsync(modelo);
        await _contexto.SaveChangesAsync();
        return modelo;
    }

    public async Task ActualizarModeloAsync(ModeloActivo modelo)
    {
        _contexto.ModelosActivo.Update(modelo);
        await _contexto.SaveChangesAsync();
    }

    // ── Estados de activo ────────────────────────────
    public async Task<IEnumerable<EstadoActivo>> ObtenerEstadosActivoAsync() =>
        await _contexto.EstadosActivo.Where(e => e.Activo).OrderBy(e => e.Nombre).AsNoTracking().ToListAsync();

    public async Task<EstadoActivo?> ObtenerEstadoActivoPorIdAsync(int id) =>
        await _contexto.EstadosActivo.FindAsync(id);

    public async Task<EstadoActivo> AgregarEstadoActivoAsync(EstadoActivo estado)
    {
        await _contexto.EstadosActivo.AddAsync(estado);
        await _contexto.SaveChangesAsync();
        return estado;
    }

    public async Task ActualizarEstadoActivoAsync(EstadoActivo estado)
    {
        _contexto.EstadosActivo.Update(estado);
        await _contexto.SaveChangesAsync();
    }
}
