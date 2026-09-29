namespace Aduasis.Api.DTOs.Respuestas;

/// <summary>
/// Datos de una asignación para mostrar en la interfaz.
/// </summary>
public class RespuestaAsignacion
{
    public int Id { get; set; }
    public int ActivoId { get; set; }
    public int? UsuarioId { get; set; }
    public string? NombreUsuario { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public bool EstaActiva => FechaFin is null;
    public string? Observaciones { get; set; }
    public string? RegistradoPor { get; set; }
    public DateTime CreadoEn { get; set; }
}
