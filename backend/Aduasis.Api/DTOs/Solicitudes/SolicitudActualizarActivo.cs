namespace Aduasis.Api.DTOs.Solicitudes;

/// <summary>
/// Datos para actualizar un activo existente.
/// Todos los campos son opcionales — solo se actualizan los que se envíen.
/// </summary>
public class SolicitudActualizarActivo
{
    public string? NombreDescriptivo { get; set; }
    public string? Especificaciones { get; set; }
    public string? Color { get; set; }

    public int? TipoActivoId { get; set; }
    public int? MarcaId { get; set; }
    public int? ModeloActivoId { get; set; }
    public int? EstadoActivoId { get; set; }

    public int? EspacioId { get; set; }
    public int? AreaId { get; set; }
    public int? UsuarioAsignadoId { get; set; }

    public string? DireccionIp { get; set; }
    public DateOnly? FechaAdquisicion { get; set; }
    public string? Observaciones { get; set; }
}
