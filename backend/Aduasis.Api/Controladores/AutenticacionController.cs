using Aduasis.Api.DTOs.Solicitudes;
using Aduasis.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aduasis.Api.Controladores;

/// <summary>
/// Controlador de autenticación.
/// Responsabilidades: recibir la solicitud, invocar el servicio, devolver la respuesta.
/// La lógica de negocio (validar contraseña, generar token) está en ServicioAutenticacion.
/// </summary>
[ApiController]
[Route("api/autenticacion")]
public class AutenticacionController : ControllerBase
{
    private readonly IServicioAutenticacion _servicioAutenticacion;

    public AutenticacionController(IServicioAutenticacion servicioAutenticacion)
    {
        _servicioAutenticacion = servicioAutenticacion;
    }

    /// <summary>
    /// Inicia sesión con correo o nombre de usuario y contraseña.
    /// Devuelve un access token JWT y un refresh token.
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] SolicitudLogin solicitud)
    {
        try
        {
            var ipOrigen = HttpContext.Connection.RemoteIpAddress?.ToString();
            var respuesta = await _servicioAutenticacion.IniciarSesionAsync(solicitud, ipOrigen);
            return Ok(respuesta);
        }
        catch (UnauthorizedAccessException)
        {
            // Mensaje genérico para no revelar si el usuario existe o no
            return Unauthorized(new { mensaje = "Credenciales inválidas." });
        }
    }

    /// <summary>
    /// Renueva el access token usando un refresh token válido.
    /// </summary>
    [HttpPost("refrescar")]
    public async Task<IActionResult> Refrescar([FromBody] SolicitudRefrescarToken solicitud)
    {
        try
        {
            var ipOrigen = HttpContext.Connection.RemoteIpAddress?.ToString();
            var respuesta = await _servicioAutenticacion.RefrescarTokenAsync(solicitud, ipOrigen);
            return Ok(respuesta);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { mensaje = ex.Message });
        }
    }

    /// <summary>
    /// Cierra la sesión revocando todos los tokens activos del usuario.
    /// </summary>
    [HttpPost("cerrar-sesion")]
    public async Task<IActionResult> CerrarSesion([FromBody] SolicitudRefrescarToken solicitud)
    {
        await _servicioAutenticacion.CerrarSesionAsync(solicitud.TokenRefresco);

        // Siempre devolvemos 200 aunque el token no exista,
        // para no revelar información sobre tokens válidos
        return Ok(new { mensaje = "Sesión cerrada correctamente." });
    }
}
