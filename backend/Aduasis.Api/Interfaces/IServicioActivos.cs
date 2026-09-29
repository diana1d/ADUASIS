using Aduasis.Api.DTOs.Respuestas;
using Aduasis.Api.DTOs.Solicitudes;

namespace Aduasis.Api.Interfaces;

/// <summary>
/// Contrato del servicio de activos tecnológicos.
/// </summary>
public interface IServicioActivos
{
    Task<IEnumerable<RespuestaActivo>> ObtenerTodosAsync();
    Task<RespuestaActivo> ObtenerPorIdAsync(int id);
    Task<RespuestaActivo> CrearAsync(SolicitudCrearActivo solicitud, int usuarioId);
    Task<RespuestaActivo> ActualizarAsync(int id, SolicitudActualizarActivo solicitud, int usuarioId);
    Task EliminarAsync(int id);
}
