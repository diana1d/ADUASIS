using Aduasis.Api.Datos;
using Aduasis.Api.Interfaces;
using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Aduasis.Api.Repositorios;

public class RepositorioAsignaciones : IRepositorioAsignaciones
{
    private readonly ContextoAduasis _contexto;

    public RepositorioAsignaciones(ContextoAduasis contexto)
    {
        _contexto = contexto;
    }

    public async Task<IEnumerable<Asignacion>> ObtenerPorActivoAsync(int activoId)
    {
        return await _contexto.Asignaciones
            .Include(a => a.Usuario)
            .Include(a => a.RegistradoPor)
            .Where(a => a.ActivoId == activoId)
            .OrderByDescending(a => a.FechaInicio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Asignacion?> ObtenerActivaAsync(int activoId)
    {
        return await _contexto.Asignaciones
            .Include(a => a.Usuario)
            .FirstOrDefaultAsync(a => a.ActivoId == activoId && a.FechaFin == null);
    }

    public async Task AgregarAsync(Asignacion asignacion)
    {
        await _contexto.Asignaciones.AddAsync(asignacion);
        await _contexto.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Asignacion asignacion)
    {
        _contexto.Asignaciones.Update(asignacion);
        await _contexto.SaveChangesAsync();
    }
}
