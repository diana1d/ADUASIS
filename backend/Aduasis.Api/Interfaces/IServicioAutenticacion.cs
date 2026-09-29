using Aduasis.Api.DTOs.Solicitudes;
using Aduasis.Api.DTOs.Respuestas;

namespace Aduasis.Api.Interfaces;

/// <summary>
/// Contrato del servicio de autenticación.
/// Define las operaciones de login, refresco y cierre de sesión.
/// </summary>
public interface IServicioAutenticacion
{
    Task<RespuestaAutenticacion> IniciarSesionAsync(SolicitudLogin solicitud, string? ipOrigen);
    Task<RespuestaAutenticacion> RefrescarTokenAsync(SolicitudRefrescarToken solicitud, string? ipOrigen);
    Task CerrarSesionAsync(string tokenRefresco);
}
