using Aduasis.Api.Datos;
using Aduasis.Api.Interfaces;
using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Aduasis.Api.Repositorios;

public class RepositorioHistorial : IRepositorioHistorial
{
    private readonly ContextoAduasis _contexto;

    public RepositorioHistorial(ContextoAduasis contexto)
    {
        _contexto = contexto;
    }

    public async Task<IEnumerable<HistorialActivo>> ObtenerPorActivoAsync(int activoId)
    {
        return await _contexto.HistorialActivos
            .Include(h => h.Usuario)
            .Where(h => h.ActivoId == activoId)
            .OrderByDescending(h => h.CreadoEn)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task RegistrarAsync(HistorialActivo evento)
    {
        await _contexto.HistorialActivos.AddAsync(evento);
        await _contexto.SaveChangesAsync();
    }
}
