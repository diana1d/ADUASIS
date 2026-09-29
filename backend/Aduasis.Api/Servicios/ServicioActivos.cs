using Aduasis.Api.DTOs.Respuestas;
using Aduasis.Api.DTOs.Solicitudes;
using Aduasis.Api.Interfaces;
using Aduasis.Api.Modelos;

namespace Aduasis.Api.Servicios;

/// <summary>
/// Servicio de activos tecnológicos.
/// Contiene la lógica de negocio: validaciones, mapeo y orquestación.
/// El controlador solo invoca este servicio y devuelve la respuesta.
/// </summary>
public class ServicioActivos : IServicioActivos
{
    private readonly IRepositorioActivos _repoActivos;

    public ServicioActivos(IRepositorioActivos repoActivos)
    {
        _repoActivos = repoActivos;
    }

    public async Task<IEnumerable<RespuestaActivo>> ObtenerTodosAsync()
    {
        var activos = await _repoActivos.ObtenerTodosAsync();
        return activos.Select(MapearARespuesta);
    }

    public async Task<RespuestaActivo> ObtenerPorIdAsync(int id)
    {
        var activo = await _repoActivos.ObtenerPorIdAsync(id)
            ?? throw new KeyNotFoundException($"No se encontró el activo con Id {id}.");

        return MapearARespuesta(activo);
    }

    public async Task<RespuestaActivo> CrearAsync(SolicitudCrearActivo solicitud, int usuarioId)
    {
        // Validar unicidad del código de activo
        if (await _repoActivos.ExisteCodigoAsync(solicitud.CodigoActivo))
            throw new InvalidOperationException($"Ya existe un activo con el código '{solicitud.CodigoActivo}'.");

        // Validar unicidad del código QR si se proporcionó
        if (!string.IsNullOrWhiteSpace(solicitud.CodigoQr) &&
            await _repoActivos.ExisteCodigoQrAsync(solicitud.CodigoQr))
            throw new InvalidOperationException($"Ya existe un activo con el código QR '{solicitud.CodigoQr}'.");

        var activo = new Activo
        {
            CodigoActivo       = solicitud.CodigoActivo.Trim(),
            CodigoQr           = solicitud.CodigoQr?.Trim(),
            NombreDescriptivo  = solicitud.NombreDescriptivo.Trim(),
            Especificaciones   = solicitud.Especificaciones?.Trim(),
            Color              = solicitud.Color?.Trim(),
            TipoActivoId       = solicitud.TipoActivoId,
            MarcaId            = solicitud.MarcaId,
            ModeloActivoId     = solicitud.ModeloActivoId,
            EstadoActivoId     = solicitud.EstadoActivoId,
            EspacioId          = solicitud.EspacioId,
            AreaId             = solicitud.AreaId,
            UsuarioAsignadoId  = solicitud.UsuarioAsignadoId,
            DireccionIp        = solicitud.DireccionIp?.Trim(),
            FechaAdquisicion   = solicitud.FechaAdquisicion,
            Observaciones      = solicitud.Observaciones?.Trim(),
            CreadoPorId        = usuarioId,
            CreadoEn           = DateTime.UtcNow,
            ActualizadoEn      = DateTime.UtcNow
        };

        var creado = await _repoActivos.AgregarAsync(activo);

        // Recargar con relaciones para devolver la respuesta completa
        var conRelaciones = await _repoActivos.ObtenerPorIdAsync(creado.Id)
            ?? throw new InvalidOperationException("Error al recuperar el activo creado.");

        return MapearARespuesta(conRelaciones);
    }

    public async Task<RespuestaActivo> ActualizarAsync(int id, SolicitudActualizarActivo solicitud)
    {
        var activo = await _repoActivos.ObtenerPorIdAsync(id)
            ?? throw new KeyNotFoundException($"No se encontró el activo con Id {id}.");

        // Aplicar solo los campos que se enviaron (no null)
        if (solicitud.NombreDescriptivo is not null)
            activo.NombreDescriptivo = solicitud.NombreDescriptivo.Trim();

        if (solicitud.Especificaciones is not null)
            activo.Especificaciones = solicitud.Especificaciones.Trim();

        if (solicitud.Color is not null)
            activo.Color = solicitud.Color.Trim();

        if (solicitud.TipoActivoId.HasValue)
            activo.TipoActivoId = solicitud.TipoActivoId.Value;

        if (solicitud.MarcaId.HasValue)
            activo.MarcaId = solicitud.MarcaId.Value;

        if (solicitud.ModeloActivoId.HasValue)
            activo.ModeloActivoId = solicitud.ModeloActivoId.Value;

        if (solicitud.EstadoActivoId.HasValue)
            activo.EstadoActivoId = solicitud.EstadoActivoId.Value;

        if (solicitud.EspacioId.HasValue)
            activo.EspacioId = solicitud.EspacioId.Value;

        if (solicitud.AreaId.HasValue)
            activo.AreaId = solicitud.AreaId.Value;

        if (solicitud.UsuarioAsignadoId.HasValue)
            activo.UsuarioAsignadoId = solicitud.UsuarioAsignadoId.Value;

        if (solicitud.DireccionIp is not null)
            activo.DireccionIp = solicitud.DireccionIp.Trim();

        if (solicitud.FechaAdquisicion.HasValue)
            activo.FechaAdquisicion = solicitud.FechaAdquisicion.Value;

        if (solicitud.Observaciones is not null)
            activo.Observaciones = solicitud.Observaciones.Trim();

        activo.ActualizadoEn = DateTime.UtcNow;

        await _repoActivos.ActualizarAsync(activo);

        var actualizado = await _repoActivos.ObtenerPorIdAsync(id)!;
        return MapearARespuesta(actualizado!);
    }

    public async Task EliminarAsync(int id)
    {
        var activo = await _repoActivos.ObtenerPorIdAsync(id)
            ?? throw new KeyNotFoundException($"No se encontró el activo con Id {id}.");

        await _repoActivos.EliminarAsync(activo);
    }

    // ── Mapeo privado: Modelo → DTO de respuesta ─────
    private static RespuestaActivo MapearARespuesta(Activo activo) => new()
    {
        Id                = activo.Id,
        CodigoActivo      = activo.CodigoActivo,
        CodigoQr          = activo.CodigoQr,
        NombreDescriptivo = activo.NombreDescriptivo,
        Especificaciones  = activo.Especificaciones,
        Color             = activo.Color,

        TipoActivoId  = activo.TipoActivoId,
        TipoActivo    = activo.TipoActivo?.Nombre ?? string.Empty,
        MarcaId       = activo.MarcaId,
        Marca         = activo.Marca?.Nombre ?? string.Empty,
        ModeloActivoId = activo.ModeloActivoId,
        ModeloActivo  = activo.ModeloActivo?.Nombre,

        EstadoActivoId = activo.EstadoActivoId,
        EstadoActivo   = activo.EstadoActivo?.Nombre ?? string.Empty,

        EspacioId = activo.EspacioId,
        Espacio   = activo.Espacio?.Nombre,
        AreaId    = activo.AreaId,
        Area      = activo.Area?.Nombre,
        Piso      = activo.Espacio?.Piso?.Nombre,
        Edificio  = activo.Espacio?.Piso?.Edificio?.Nombre,

        UsuarioAsignadoId = activo.UsuarioAsignadoId,
        UsuarioAsignado   = activo.UsuarioAsignado is not null
            ? $"{activo.UsuarioAsignado.Nombre} {activo.UsuarioAsignado.Apellido}"
            : null,

        DireccionIp      = activo.DireccionIp,
        FechaAdquisicion = activo.FechaAdquisicion,
        Observaciones    = activo.Observaciones,
        CreadoEn         = activo.CreadoEn,
        ActualizadoEn    = activo.ActualizadoEn
    };
}
