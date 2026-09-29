using Aduasis.Api.DTOs.Solicitudes;
using Aduasis.Api.Interfaces;
using Aduasis.Api.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aduasis.Api.Controladores;

/// <summary>
/// Controlador de historial y asignaciones de activos.
/// </summary>
[ApiController]
[Route("api/activos/{activoId:int}")]
[Authorize]
public class HistorialController : ControllerBase
{
    private readonly IServicioHistorial _servicioHistorial;

    public HistorialController(IServicioHistorial servicioHistorial)
    {
        _servicioHistorial = servicioHistorial;
    }

    // ── Historial ────────────────────────────────────

    /// <summary>Obtiene el historial completo de un activo.</summary>
    [HttpGet("historial")]
    public async Task<IActionResult> ObtenerHistorial(int activoId)
    {
        try
        {
            var historial = await _servicioHistorial.ObtenerHistorialAsync(activoId);
            return Ok(historial);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }

    /// <summary>Agrega una nota manual al historial de un activo.</summary>
    [HttpPost("historial/notas")]
    [Authorize(Roles = "Administrador,Funcionario")]
    public async Task<IActionResult> AgregarNota(int activoId, [FromBody] SolicitudNotaHistorial solicitud)
    {
        try
        {
            var usuarioId = ObtenerUsuarioId();
            await _servicioHistorial.AgregarNotaManualAsync(activoId, solicitud, usuarioId);
            return Ok(new { mensaje = "Nota registrada correctamente." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }

    // ── Asignaciones ─────────────────────────────────

    /// <summary>Obtiene el historial de asignaciones de un activo.</summary>
    [HttpGet("asignaciones")]
    public async Task<IActionResult> ObtenerAsignaciones(int activoId)
    {
        var asignaciones = await _servicioHistorial.ObtenerAsignacionesAsync(activoId);
        return Ok(asignaciones);
    }

    /// <summary>Asigna el activo a un usuario.</summary>
    [HttpPost("asignaciones")]
    [Authorize(Roles = "Administrador,Funcionario")]
    public async Task<IActionResult> Asignar(int activoId, [FromBody] SolicitudAsignacion solicitud)
    {
        try
        {
            var usuarioId = ObtenerUsuarioId();
            var asignacion = await _servicioHistorial.AsignarActivoAsync(activoId, solicitud, usuarioId);
            return Ok(asignacion);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }

    /// <summary>Desasigna el activo (queda sin usuario asignado).</summary>
    [HttpDelete("asignaciones/activa")]
    [Authorize(Roles = "Administrador,Funcionario")]
    public async Task<IActionResult> Desasignar(int activoId)
    {
        try
        {
            var usuarioId = ObtenerUsuarioId();
            await _servicioHistorial.DesasignarActivoAsync(activoId, usuarioId);
            return Ok(new { mensaje = "Activo desasignado correctamente." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }

    private int ObtenerUsuarioId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)
            ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub);
        return int.Parse(claim!.Value);
    }
}
