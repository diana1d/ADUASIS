using Aduasis.Api.Modelos;
using Aduasis.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aduasis.Api.Controladores;

/// <summary>
/// Controlador de catálogos: tipos de activo, marcas, modelos y estados.
/// Los GET son accesibles por cualquier usuario autenticado.
/// Los POST/PUT requieren rol Administrador.
/// </summary>
[ApiController]
[Route("api/catalogos")]
[Authorize]
public class CatalogosController : ControllerBase
{
    private readonly IRepositorioCatalogos _repoCatalogos;

    public CatalogosController(IRepositorioCatalogos repoCatalogos)
    {
        _repoCatalogos = repoCatalogos;
    }

    // ── Tipos de activo ──────────────────────────────

    [HttpGet("tipos-activo")]
    public async Task<IActionResult> ObtenerTiposActivo() =>
        Ok(await _repoCatalogos.ObtenerTiposActivoAsync());

    [HttpPost("tipos-activo")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CrearTipoActivo([FromBody] TipoActivo tipo)
    {
        tipo.CreadoEn = DateTime.UtcNow;
        var creado = await _repoCatalogos.AgregarTipoActivoAsync(tipo);
        return CreatedAtAction(nameof(ObtenerTiposActivo), new { id = creado.Id }, creado);
    }

    [HttpPut("tipos-activo/{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> ActualizarTipoActivo(int id, [FromBody] TipoActivo tipo)
    {
        var existente = await _repoCatalogos.ObtenerTipoActivoPorIdAsync(id);
        if (existente is null) return NotFound(new { mensaje = "Tipo de activo no encontrado." });

        existente.Nombre      = tipo.Nombre;
        existente.Descripcion = tipo.Descripcion;
        existente.Activo      = tipo.Activo;
        await _repoCatalogos.ActualizarTipoActivoAsync(existente);
        return Ok(existente);
    }

    // ── Marcas ───────────────────────────────────────

    [HttpGet("marcas")]
    public async Task<IActionResult> ObtenerMarcas() =>
        Ok(await _repoCatalogos.ObtenerMarcasAsync());

    [HttpPost("marcas")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CrearMarca([FromBody] Marca marca)
    {
        marca.CreadoEn = DateTime.UtcNow;
        var creada = await _repoCatalogos.AgregarMarcaAsync(marca);
        return CreatedAtAction(nameof(ObtenerMarcas), new { id = creada.Id }, creada);
    }

    [HttpPut("marcas/{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> ActualizarMarca(int id, [FromBody] Marca marca)
    {
        var existente = await _repoCatalogos.ObtenerMarcaPorIdAsync(id);
        if (existente is null) return NotFound(new { mensaje = "Marca no encontrada." });

        existente.Nombre = marca.Nombre;
        existente.Activo = marca.Activo;
        await _repoCatalogos.ActualizarMarcaAsync(existente);
        return Ok(existente);
    }

    // ── Modelos ──────────────────────────────────────

    [HttpGet("modelos")]
    public async Task<IActionResult> ObtenerModelos([FromQuery] int? marcaId = null) =>
        Ok(await _repoCatalogos.ObtenerModelosAsync(marcaId));

    [HttpPost("modelos")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CrearModelo([FromBody] ModeloActivo modelo)
    {
        modelo.CreadoEn = DateTime.UtcNow;
        var creado = await _repoCatalogos.AgregarModeloAsync(modelo);
        return CreatedAtAction(nameof(ObtenerModelos), new { id = creado.Id }, creado);
    }

    [HttpPut("modelos/{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> ActualizarModelo(int id, [FromBody] ModeloActivo modelo)
    {
        var existente = await _repoCatalogos.ObtenerModeloPorIdAsync(id);
        if (existente is null) return NotFound(new { mensaje = "Modelo no encontrado." });

        existente.Nombre = modelo.Nombre;
        existente.Activo = modelo.Activo;
        await _repoCatalogos.ActualizarModeloAsync(existente);
        return Ok(existente);
    }

    // ── Estados de activo ────────────────────────────

    [HttpGet("estados-activo")]
    public async Task<IActionResult> ObtenerEstadosActivo() =>
        Ok(await _repoCatalogos.ObtenerEstadosActivoAsync());

    [HttpPost("estados-activo")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CrearEstadoActivo([FromBody] EstadoActivo estado)
    {
        estado.CreadoEn = DateTime.UtcNow;
        var creado = await _repoCatalogos.AgregarEstadoActivoAsync(estado);
        return CreatedAtAction(nameof(ObtenerEstadosActivo), new { id = creado.Id }, creado);
    }

    [HttpPut("estados-activo/{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> ActualizarEstadoActivo(int id, [FromBody] EstadoActivo estado)
    {
        var existente = await _repoCatalogos.ObtenerEstadoActivoPorIdAsync(id);
        if (existente is null) return NotFound(new { mensaje = "Estado de activo no encontrado." });

        existente.Nombre      = estado.Nombre;
        existente.Descripcion = estado.Descripcion;
        existente.Activo      = estado.Activo;
        await _repoCatalogos.ActualizarEstadoActivoAsync(existente);
        return Ok(existente);
    }
}
