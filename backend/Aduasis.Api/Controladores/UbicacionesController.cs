using Aduasis.Api.Modelos;
using Aduasis.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aduasis.Api.Controladores;

/// <summary>
/// Controlador de la jerarquía de ubicaciones: Edificio → Piso → Área / Espacio.
/// </summary>
[ApiController]
[Route("api/ubicaciones")]
[Authorize]
public class UbicacionesController : ControllerBase
{
    private readonly IRepositorioUbicaciones _repoUbicaciones;

    public UbicacionesController(IRepositorioUbicaciones repoUbicaciones)
    {
        _repoUbicaciones = repoUbicaciones;
    }

    // ── Edificios ────────────────────────────────────

    [HttpGet("edificios")]
    public async Task<IActionResult> ObtenerEdificios() =>
        Ok(await _repoUbicaciones.ObtenerEdificiosAsync());

    [HttpPost("edificios")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CrearEdificio([FromBody] Edificio edificio)
    {
        edificio.CreadoEn = DateTime.UtcNow;
        var creado = await _repoUbicaciones.AgregarEdificioAsync(edificio);
        return CreatedAtAction(nameof(ObtenerEdificios), new { id = creado.Id }, creado);
    }

    [HttpPut("edificios/{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> ActualizarEdificio(int id, [FromBody] Edificio edificio)
    {
        var existente = await _repoUbicaciones.ObtenerEdificioPorIdAsync(id);
        if (existente is null) return NotFound(new { mensaje = "Edificio no encontrado." });

        existente.Nombre      = edificio.Nombre;
        existente.Descripcion = edificio.Descripcion;
        existente.Activo      = edificio.Activo;
        await _repoUbicaciones.ActualizarEdificioAsync(existente);
        return Ok(existente);
    }

    // ── Pisos ────────────────────────────────────────

    [HttpGet("pisos")]
    public async Task<IActionResult> ObtenerPisos([FromQuery] int? edificioId = null) =>
        Ok(await _repoUbicaciones.ObtenerPisosAsync(edificioId));

    [HttpPost("pisos")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CrearPiso([FromBody] Piso piso)
    {
        piso.CreadoEn = DateTime.UtcNow;
        var creado = await _repoUbicaciones.AgregarPisoAsync(piso);
        return CreatedAtAction(nameof(ObtenerPisos), new { id = creado.Id }, creado);
    }

    [HttpPut("pisos/{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> ActualizarPiso(int id, [FromBody] Piso piso)
    {
        var existente = await _repoUbicaciones.ObtenerPisoPorIdAsync(id);
        if (existente is null) return NotFound(new { mensaje = "Piso no encontrado." });

        existente.Nombre = piso.Nombre;
        existente.Orden  = piso.Orden;
        existente.Activo = piso.Activo;
        await _repoUbicaciones.ActualizarPisoAsync(existente);
        return Ok(existente);
    }

    // ── Áreas ────────────────────────────────────────

    [HttpGet("areas")]
    public async Task<IActionResult> ObtenerAreas([FromQuery] int? pisoId = null) =>
        Ok(await _repoUbicaciones.ObtenerAreasAsync(pisoId));

    [HttpPost("areas")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CrearArea([FromBody] Area area)
    {
        area.CreadoEn = DateTime.UtcNow;
        var creada = await _repoUbicaciones.AgregarAreaAsync(area);
        return CreatedAtAction(nameof(ObtenerAreas), new { id = creada.Id }, creada);
    }

    [HttpPut("areas/{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> ActualizarArea(int id, [FromBody] Area area)
    {
        var existente = await _repoUbicaciones.ObtenerAreaPorIdAsync(id);
        if (existente is null) return NotFound(new { mensaje = "Área no encontrada." });

        existente.Nombre      = area.Nombre;
        existente.Descripcion = area.Descripcion;
        existente.Activo      = area.Activo;
        await _repoUbicaciones.ActualizarAreaAsync(existente);
        return Ok(existente);
    }

    // ── Espacios ─────────────────────────────────────

    [HttpGet("espacios")]
    public async Task<IActionResult> ObtenerEspacios([FromQuery] int? pisoId = null) =>
        Ok(await _repoUbicaciones.ObtenerEspaciosAsync(pisoId));

    [HttpPost("espacios")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CrearEspacio([FromBody] Espacio espacio)
    {
        espacio.CreadoEn = DateTime.UtcNow;
        var creado = await _repoUbicaciones.AgregarEspacioAsync(espacio);
        return CreatedAtAction(nameof(ObtenerEspacios), new { id = creado.Id }, creado);
    }

    [HttpPut("espacios/{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> ActualizarEspacio(int id, [FromBody] Espacio espacio)
    {
        var existente = await _repoUbicaciones.ObtenerEspacioPorIdAsync(id);
        if (existente is null) return NotFound(new { mensaje = "Espacio no encontrado." });

        existente.Nombre      = espacio.Nombre;
        existente.Descripcion = espacio.Descripcion;
        existente.Activo      = espacio.Activo;
        await _repoUbicaciones.ActualizarEspacioAsync(existente);
        return Ok(existente);
    }
}
