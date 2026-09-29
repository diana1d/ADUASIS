using Aduasis.Api.Modelos;

namespace Aduasis.Api.Interfaces;

/// <summary>
/// Contrato para el acceso a datos del historial de activos.
/// </summary>
public interface IRepositorioHistorial
{
    Task<IEnumerable<HistorialActivo>> ObtenerPorActivoAsync(int activoId);
    Task RegistrarAsync(HistorialActivo evento);
}
