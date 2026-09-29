using Aduasis.Api.Modelos;

namespace Aduasis.Api.Interfaces;

/// <summary>
/// Contrato para el acceso a datos de asignaciones de activos.
/// </summary>
public interface IRepositorioAsignaciones
{
    Task<IEnumerable<Asignacion>> ObtenerPorActivoAsync(int activoId);
    Task<Asignacion?> ObtenerActivaAsync(int activoId);
    Task AgregarAsync(Asignacion asignacion);
    Task ActualizarAsync(Asignacion asignacion);
}
