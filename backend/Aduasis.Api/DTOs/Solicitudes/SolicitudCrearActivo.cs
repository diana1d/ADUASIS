namespace Aduasis.Api.DTOs.Solicitudes;

/// <summary>
/// Datos requeridos para registrar un nuevo activo tecnológico.
/// </summary>
public class SolicitudCrearActivo
{
    public string CodigoActivo { get; set; } = string.Empty;
    public string? CodigoQr { get; set; }
    public string NombreDescriptivo { get; set; } = string.Empty;
    public string? Especificaciones { get; set; }
    public string? Color { get; set; }

    public int TipoActivoId { get; set; }
    public int MarcaId { get; set; }
    public int? ModeloActivoId { get; set; }
    public int EstadoActivoId { get; set; }

    public int? EspacioId { get; set; }
    public int? AreaId { get; set; }
    public int? UsuarioAsignadoId { get; set; }

    public string? DireccionIp { get; set; }
    public DateOnly? FechaAdquisicion { get; set; }
    public string? Observaciones { get; set; }
}
