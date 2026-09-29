using Aduasis.Api.DTOs.Solicitudes;
using Aduasis.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aduasis.Api.Controladores;

/// <summary>
/// Controlador de activos tecnológicos.
/// GET público para consultas; POST/PUT/DELETE requieren autenticación.
/// </summary>
[ApiController]
[Route("api/activos")]
[Authorize]
public class ActivosController : ControllerBase
{
    private readonly IServicioActivos _servicioActivos;

    public ActivosController(IServicioActivos servicioActivos)
    {
        _servicioActivos = servicioActivos;
    }

    /// <summary>Obtiene todos los activos registrados.</summary>
    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var activos = await _servicioActivos.ObtenerTodosAsync();
        return Ok(activos);
    }

    /// <summary>Obtiene un activo por su Id.</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        try
        {
            var activo = await _servicioActivos.ObtenerPorIdAsync(id);
            return Ok(activo);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }

    /// <summary>Registra un nuevo activo tecnológico.</summary>
    [HttpPost]
    [Authorize(Roles = "Administrador,Funcionario")]
    public async Task<IActionResult> Crear([FromBody] SolicitudCrearActivo solicitud)
    {
        try
        {
            var usuarioId = ObtenerUsuarioId();
            var activo = await _servicioActivos.CrearAsync(solicitud, usuarioId);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = activo.Id }, activo);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }

    /// <summary>Actualiza los datos de un activo existente.</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador,Funcionario")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] SolicitudActualizarActivo solicitud)
    {
        try
        {
            var activo = await _servicioActivos.ActualizarAsync(id, solicitud);
            return Ok(activo);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }

    /// <summary>Elimina un activo. Solo Administrador.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Eliminar(int id)
    {
        try
        {
            await _servicioActivos.EliminarAsync(id);
            return NoContent();
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
