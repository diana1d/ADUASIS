using Aduasis.Api.Datos;
using Aduasis.Api.Interfaces;
using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Aduasis.Api.Repositorios;

public class RepositorioUbicaciones : IRepositorioUbicaciones
{
    private readonly ContextoAduasis _contexto;

    public RepositorioUbicaciones(ContextoAduasis contexto)
    {
        _contexto = contexto;
    }

    // ── Edificios ────────────────────────────────────
    public async Task<IEnumerable<Edificio>> ObtenerEdificiosAsync() =>
        await _contexto.Edificios.Where(e => e.Activo).OrderBy(e => e.Nombre).AsNoTracking().ToListAsync();

    public async Task<Edificio?> ObtenerEdificioPorIdAsync(int id) =>
        await _contexto.Edificios.FindAsync(id);

    public async Task<Edificio> AgregarEdificioAsync(Edificio edificio)
    {
        await _contexto.Edificios.AddAsync(edificio);
        await _contexto.SaveChangesAsync();
        return edificio;
    }

    public async Task ActualizarEdificioAsync(Edificio edificio)
    {
        _contexto.Edificios.Update(edificio);
        await _contexto.SaveChangesAsync();
    }

    // ── Pisos ────────────────────────────────────────
    public async Task<IEnumerable<Piso>> ObtenerPisosAsync(int? edificioId = null)
    {
        var consulta = _contexto.Pisos
            .Include(p => p.Edificio)
            .Where(p => p.Activo)
            .AsNoTracking();

        if (edificioId.HasValue)
            consulta = consulta.Where(p => p.EdificioId == edificioId.Value);

        return await consulta.OrderBy(p => p.Orden).ToListAsync();
    }

    public async Task<Piso?> ObtenerPisoPorIdAsync(int id) =>
        await _contexto.Pisos.Include(p => p.Edificio).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Piso> AgregarPisoAsync(Piso piso)
    {
        await _contexto.Pisos.AddAsync(piso);
        await _contexto.SaveChangesAsync();
        return piso;
    }

    public async Task ActualizarPisoAsync(Piso piso)
    {
        _contexto.Pisos.Update(piso);
        await _contexto.SaveChangesAsync();
    }

    // ── Áreas ────────────────────────────────────────
    public async Task<IEnumerable<Area>> ObtenerAreasAsync(int? pisoId = null)
    {
        var consulta = _contexto.Areas
            .Include(a => a.Piso).ThenInclude(p => p.Edificio)
            .Where(a => a.Activo)
            .AsNoTracking();

        if (pisoId.HasValue)
            consulta = consulta.Where(a => a.PisoId == pisoId.Value);

        return await consulta.OrderBy(a => a.Nombre).ToListAsync();
    }

    public async Task<Area?> ObtenerAreaPorIdAsync(int id) =>
        await _contexto.Areas.Include(a => a.Piso).FirstOrDefaultAsync(a => a.Id == id);

    public async Task<Area> AgregarAreaAsync(Area area)
    {
        await _contexto.Areas.AddAsync(area);
        await _contexto.SaveChangesAsync();
        return area;
    }

    public async Task ActualizarAreaAsync(Area area)
    {
        _contexto.Areas.Update(area);
        await _contexto.SaveChangesAsync();
    }

    // ── Espacios ─────────────────────────────────────
    public async Task<IEnumerable<Espacio>> ObtenerEspaciosAsync(int? pisoId = null)
    {
        var consulta = _contexto.Espacios
            .Include(e => e.Piso).ThenInclude(p => p.Edificio)
            .Where(e => e.Activo)
            .AsNoTracking();

        if (pisoId.HasValue)
            consulta = consulta.Where(e => e.PisoId == pisoId.Value);

        return await consulta.OrderBy(e => e.Nombre).ToListAsync();
    }

    public async Task<Espacio?> ObtenerEspacioPorIdAsync(int id) =>
        await _contexto.Espacios.Include(e => e.Piso).FirstOrDefaultAsync(e => e.Id == id);

    public async Task<Espacio> AgregarEspacioAsync(Espacio espacio)
    {
        await _contexto.Espacios.AddAsync(espacio);
        await _contexto.SaveChangesAsync();
        return espacio;
    }

    public async Task ActualizarEspacioAsync(Espacio espacio)
    {
        _contexto.Espacios.Update(espacio);
        await _contexto.SaveChangesAsync();
    }
}
