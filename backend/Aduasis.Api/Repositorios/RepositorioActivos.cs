using Aduasis.Api.Datos;
using Aduasis.Api.Interfaces;
using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Aduasis.Api.Repositorios;

/// <summary>
/// Acceso a datos de activos. Incluye todas las relaciones necesarias
/// para evitar múltiples consultas al renderizar la lista.
/// </summary>
public class RepositorioActivos : IRepositorioActivos
{
    private readonly ContextoAduasis _contexto;

    public RepositorioActivos(ContextoAduasis contexto)
    {
        _contexto = contexto;
    }

    public async Task<IEnumerable<Activo>> ObtenerTodosAsync()
    {
        return await _contexto.Activos
            .Include(a => a.TipoActivo)
            .Include(a => a.Marca)
            .Include(a => a.ModeloActivo)
            .Include(a => a.EstadoActivo)
            .Include(a => a.Espacio).ThenInclude(e => e!.Piso).ThenInclude(p => p.Edificio)
            .Include(a => a.Area)
            .Include(a => a.UsuarioAsignado)
            .AsNoTracking()
            .OrderBy(a => a.CodigoActivo)
            .ToListAsync();
    }

    public async Task<Activo?> ObtenerPorIdAsync(int id)
    {
        return await _contexto.Activos
            .Include(a => a.TipoActivo)
            .Include(a => a.Marca)
            .Include(a => a.ModeloActivo)
            .Include(a => a.EstadoActivo)
            .Include(a => a.Espacio).ThenInclude(e => e!.Piso).ThenInclude(p => p.Edificio)
            .Include(a => a.Area)
            .Include(a => a.UsuarioAsignado)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Activo?> ObtenerPorCodigoAsync(string codigoActivo)
    {
        return await _contexto.Activos
            .Include(a => a.TipoActivo)
            .Include(a => a.Marca)
            .Include(a => a.ModeloActivo)
            .Include(a => a.EstadoActivo)
            .Include(a => a.Espacio).ThenInclude(e => e!.Piso).ThenInclude(p => p.Edificio)
            .Include(a => a.Area)
            .Include(a => a.UsuarioAsignado)
            .FirstOrDefaultAsync(a => a.CodigoActivo == codigoActivo);
    }

    public async Task<bool> ExisteCodigoAsync(string codigoActivo, int? excluirId = null)
    {
        return await _contexto.Activos
            .AnyAsync(a => a.CodigoActivo == codigoActivo && a.Id != excluirId);
    }

    public async Task<bool> ExisteCodigoQrAsync(string codigoQr, int? excluirId = null)
    {
        return await _contexto.Activos
            .AnyAsync(a => a.CodigoQr == codigoQr && a.Id != excluirId);
    }

    public async Task<Activo> AgregarAsync(Activo activo)
    {
        await _contexto.Activos.AddAsync(activo);
        await _contexto.SaveChangesAsync();
        return activo;
    }

    public async Task ActualizarAsync(Activo activo)
    {
        _contexto.Activos.Update(activo);
        await _contexto.SaveChangesAsync();
    }

    public async Task EliminarAsync(Activo activo)
    {
        _contexto.Activos.Remove(activo);
        await _contexto.SaveChangesAsync();
    }
}
