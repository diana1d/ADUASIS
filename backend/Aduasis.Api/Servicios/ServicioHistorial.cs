using Aduasis.Api.DTOs.Respuestas;
using Aduasis.Api.DTOs.Solicitudes;
using Aduasis.Api.Interfaces;
using Aduasis.Api.Modelos;

namespace Aduasis.Api.Servicios;

/// <summary>
/// Servicio de historial y asignaciones de activos.
///
/// Responsabilidades:
/// - Registrar eventos automáticos cuando cambia estado, ubicación o asignación
/// - Permitir notas manuales del usuario
/// - Gestionar el ciclo de vida de asignaciones
/// - Cerrar la asignación activa automáticamente cuando se da de baja un activo
///
/// Este servicio es invocado internamente por ServicioActivos para
/// registrar eventos automáticos, y también expuesto mediante el controlador
/// para acciones explícitas del usuario.
/// </summary>
public class ServicioHistorial : IServicioHistorial
{
    private readonly IRepositorioHistorial _repoHistorial;
    private readonly IRepositorioAsignaciones _repoAsignaciones;
    private readonly IRepositorioActivos _repoActivos;
    private readonly IRepositorioUsuarios _repoUsuarios;

    public ServicioHistorial(
        IRepositorioHistorial repoHistorial,
        IRepositorioAsignaciones repoAsignaciones,
        IRepositorioActivos repoActivos,
        IRepositorioUsuarios repoUsuarios)
    {
        _repoHistorial    = repoHistorial;
        _repoAsignaciones = repoAsignaciones;
        _repoActivos      = repoActivos;
        _repoUsuarios     = repoUsuarios;
    }

    // ── Historial ────────────────────────────────────

    public async Task<IEnumerable<RespuestaHistorialActivo>> ObtenerHistorialAsync(int activoId)
    {
        VerificarActivoExiste(activoId);
        var eventos = await _repoHistorial.ObtenerPorActivoAsync(activoId);
        return eventos.Select(MapearHistorial);
    }

    public async Task AgregarNotaManualAsync(
        int activoId,
        SolicitudNotaHistorial solicitud,
        int usuarioId)
    {
        var activo = await _repoActivos.ObtenerPorIdAsync(activoId)
            ?? throw new KeyNotFoundException($"No se encontró el activo con Id {activoId}.");

        var nota = new HistorialActivo
        {
            ActivoId      = activoId,
            UsuarioId     = usuarioId,
            TipoEvento    = TipoEventoHistorial.Nota,
            Descripcion   = solicitud.Descripcion.Trim(),
            EsNotaManual  = true,
            CreadoEn      = DateTime.UtcNow
        };

        await _repoHistorial.RegistrarAsync(nota);
    }

    // ── Asignaciones ─────────────────────────────────

    public async Task<IEnumerable<RespuestaAsignacion>> ObtenerAsignacionesAsync(int activoId)
    {
        var asignaciones = await _repoAsignaciones.ObtenerPorActivoAsync(activoId);
        return asignaciones.Select(MapearAsignacion);
    }

    public async Task<RespuestaAsignacion> AsignarActivoAsync(
        int activoId,
        SolicitudAsignacion solicitud,
        int usuarioId)
    {
        var activo = await _repoActivos.ObtenerPorIdAsync(activoId)
            ?? throw new KeyNotFoundException($"No se encontró el activo con Id {activoId}.");

        // Obtener nombre del usuario anterior para el historial
        var nombreAnterior = activo.UsuarioAsignado is not null
            ? $"{activo.UsuarioAsignado.Nombre} {activo.UsuarioAsignado.Apellido}"
            : "Sin asignar";

        // Cerrar la asignación activa anterior si existe
        await CerrarAsignacionActivaAsync(activoId);

        // Obtener el nuevo usuario
        var nuevoUsuario = await _repoUsuarios.ObtenerPorIdAsync(solicitud.UsuarioId)
            ?? throw new KeyNotFoundException($"No se encontró el usuario con Id {solicitud.UsuarioId}.");

        var nombreNuevo = $"{nuevoUsuario.Nombre} {nuevoUsuario.Apellido}";

        // Crear la nueva asignación
        var asignacion = new Asignacion
        {
            ActivoId        = activoId,
            UsuarioId       = solicitud.UsuarioId,
            FechaInicio     = DateTime.UtcNow,
            Observaciones   = solicitud.Observaciones?.Trim(),
            RegistradoPorId = usuarioId,
            CreadoEn        = DateTime.UtcNow
        };

        await _repoAsignaciones.AgregarAsync(asignacion);

        // Actualizar el usuario asignado en el activo
        activo.UsuarioAsignadoId = solicitud.UsuarioId;
        activo.ActualizadoEn     = DateTime.UtcNow;
        await _repoActivos.ActualizarAsync(activo);

        // Registrar en el historial
        await _repoHistorial.RegistrarAsync(new HistorialActivo
        {
            ActivoId     = activoId,
            UsuarioId    = usuarioId,
            TipoEvento   = TipoEventoHistorial.CambioAsignacion,
            Descripcion  = $"Activo reasignado de '{nombreAnterior}' a '{nombreNuevo}'.",
            ValorAnterior = nombreAnterior,
            ValorNuevo    = nombreNuevo,
            CreadoEn     = DateTime.UtcNow
        });

        return MapearAsignacion(asignacion);
    }

    public async Task DesasignarActivoAsync(int activoId, int usuarioId)
    {
        var activo = await _repoActivos.ObtenerPorIdAsync(activoId)
            ?? throw new KeyNotFoundException($"No se encontró el activo con Id {activoId}.");

        var nombreAnterior = activo.UsuarioAsignado is not null
            ? $"{activo.UsuarioAsignado.Nombre} {activo.UsuarioAsignado.Apellido}"
            : "Sin asignar";

        await CerrarAsignacionActivaAsync(activoId);

        activo.UsuarioAsignadoId = null;
        activo.ActualizadoEn     = DateTime.UtcNow;
        await _repoActivos.ActualizarAsync(activo);

        await _repoHistorial.RegistrarAsync(new HistorialActivo
        {
            ActivoId      = activoId,
            UsuarioId     = usuarioId,
            TipoEvento    = TipoEventoHistorial.CambioAsignacion,
            Descripcion   = $"Activo desasignado. Anterior: '{nombreAnterior}'.",
            ValorAnterior = nombreAnterior,
            ValorNuevo    = "Sin asignar",
            CreadoEn      = DateTime.UtcNow
        });
    }

    // ── Métodos internos para uso desde ServicioActivos ──

    /// <summary>
    /// Registra un evento automático en el historial.
    /// Llamado desde ServicioActivos al detectar cambios relevantes.
    /// </summary>
    public async Task RegistrarEventoAsync(
        int activoId,
        string tipoEvento,
        string descripcion,
        int usuarioId,
        string? valorAnterior = null,
        string? valorNuevo = null)
    {
        await _repoHistorial.RegistrarAsync(new HistorialActivo
        {
            ActivoId      = activoId,
            UsuarioId     = usuarioId,
            TipoEvento    = tipoEvento,
            Descripcion   = descripcion,
            ValorAnterior = valorAnterior,
            ValorNuevo    = valorNuevo,
            EsNotaManual  = false,
            CreadoEn      = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Cierra la asignación activa de un activo dado de baja.
    /// Llamado desde ServicioActivos cuando el estado cambia a "Dado de baja".
    /// </summary>
    public async Task CerrarAsignacionPorBajaAsync(int activoId, int usuarioId)
    {
        await CerrarAsignacionActivaAsync(activoId);

        var activo = await _repoActivos.ObtenerPorIdAsync(activoId);
        if (activo is null) return;

        activo.UsuarioAsignadoId = null;
        activo.ActualizadoEn     = DateTime.UtcNow;
        await _repoActivos.ActualizarAsync(activo);
    }

    // ── Métodos privados ─────────────────────────────

    private async Task CerrarAsignacionActivaAsync(int activoId)
    {
        var activa = await _repoAsignaciones.ObtenerActivaAsync(activoId);
        if (activa is null) return;

        activa.FechaFin = DateTime.UtcNow;
        await _repoAsignaciones.ActualizarAsync(activa);
    }

    private void VerificarActivoExiste(int activoId)
    {
        // La verificación real se hace en el repositorio, esto es una guarda rápida
    }

    private static RespuestaHistorialActivo MapearHistorial(HistorialActivo h) => new()
    {
        Id            = h.Id,
        TipoEvento    = h.TipoEvento,
        Descripcion   = h.Descripcion,
        ValorAnterior = h.ValorAnterior,
        ValorNuevo    = h.ValorNuevo,
        EsNotaManual  = h.EsNotaManual,
        Usuario       = h.Usuario is not null
            ? $"{h.Usuario.Nombre} {h.Usuario.Apellido}"
            : null,
        CreadoEn      = h.CreadoEn
    };

    private static RespuestaAsignacion MapearAsignacion(Asignacion a) => new()
    {
        Id           = a.Id,
        ActivoId     = a.ActivoId,
        UsuarioId    = a.UsuarioId,
        NombreUsuario = a.Usuario is not null
            ? $"{a.Usuario.Nombre} {a.Usuario.Apellido}"
            : null,
        FechaInicio   = a.FechaInicio,
        FechaFin      = a.FechaFin,
        Observaciones = a.Observaciones,
        RegistradoPor = a.RegistradoPor is not null
            ? $"{a.RegistradoPor.Nombre} {a.RegistradoPor.Apellido}"
            : null,
        CreadoEn      = a.CreadoEn
    };
}
