using Aduasis.Api.DTOs.Respuestas;
using Aduasis.Api.DTOs.Solicitudes;

namespace Aduasis.Api.Interfaces;

/// <summary>
/// Contrato del servicio de historial y asignaciones.
/// </summary>
public interface IServicioHistorial
{
    // Historial
    Task<IEnumerable<RespuestaHistorialActivo>> ObtenerHistorialAsync(int activoId);
    Task AgregarNotaManualAsync(int activoId, SolicitudNotaHistorial solicitud, int usuarioId);

    // Asignaciones
    Task<IEnumerable<RespuestaAsignacion>> ObtenerAsignacionesAsync(int activoId);
    Task<RespuestaAsignacion> AsignarActivoAsync(int activoId, SolicitudAsignacion solicitud, int usuarioId);
    Task DesasignarActivoAsync(int activoId, int usuarioId);
}
