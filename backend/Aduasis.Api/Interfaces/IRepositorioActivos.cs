using Aduasis.Api.Modelos;

namespace Aduasis.Api.Interfaces;

/// <summary>
/// Contrato para el acceso a datos de activos tecnológicos.
/// </summary>
public interface IRepositorioActivos
{
    Task<IEnumerable<Activo>> ObtenerTodosAsync();
    Task<Activo?> ObtenerPorIdAsync(int id);
    Task<Activo?> ObtenerPorCodigoAsync(string codigoActivo);
    Task<bool> ExisteCodigoAsync(string codigoActivo, int? excluirId = null);
    Task<bool> ExisteCodigoQrAsync(string codigoQr, int? excluirId = null);
    Task<Activo> AgregarAsync(Activo activo);
    Task ActualizarAsync(Activo activo);
    Task EliminarAsync(Activo activo);
}
