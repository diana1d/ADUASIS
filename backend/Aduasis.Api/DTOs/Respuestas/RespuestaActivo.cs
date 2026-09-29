namespace Aduasis.Api.DTOs.Respuestas;

/// <summary>
/// Datos de un activo que se devuelven al frontend.
/// Incluye los nombres de las relaciones para evitar múltiples llamadas al API.
/// </summary>
public class RespuestaActivo
{
    public int Id { get; set; }
    public string CodigoActivo { get; set; } = string.Empty;
    public string? CodigoQr { get; set; }
    public string NombreDescriptivo { get; set; } = string.Empty;
    public string? Especificaciones { get; set; }
    public string? Color { get; set; }

    // Clasificación — id y nombre para que el frontend pueda mostrar el nombre
    // sin necesidad de otra llamada al API
    public int TipoActivoId { get; set; }
    public string TipoActivo { get; set; } = string.Empty;
    public int MarcaId { get; set; }
    public string Marca { get; set; } = string.Empty;
    public int? ModeloActivoId { get; set; }
    public string? ModeloActivo { get; set; }

    // Estado
    public int EstadoActivoId { get; set; }
    public string EstadoActivo { get; set; } = string.Empty;

    // Ubicación
    public int? EspacioId { get; set; }
    public string? Espacio { get; set; }
    public int? AreaId { get; set; }
    public string? Area { get; set; }
    public string? Piso { get; set; }
    public string? Edificio { get; set; }

    // Asignación
    public int? UsuarioAsignadoId { get; set; }
    public string? UsuarioAsignado { get; set; }

    // Red
    public string? DireccionIp { get; set; }

    // Administrativo
    public DateOnly? FechaAdquisicion { get; set; }
    public string? Observaciones { get; set; }

    // Auditoría
    public DateTime CreadoEn { get; set; }
    public DateTime ActualizadoEn { get; set; }
}
