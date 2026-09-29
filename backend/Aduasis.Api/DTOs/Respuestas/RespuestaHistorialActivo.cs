namespace Aduasis.Api.DTOs.Respuestas;

/// <summary>
/// Entrada del historial de un activo para mostrar en la interfaz.
/// </summary>
public class RespuestaHistorialActivo
{
    public int Id { get; set; }
    public string TipoEvento { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string? ValorAnterior { get; set; }
    public string? ValorNuevo { get; set; }
    public bool EsNotaManual { get; set; }
    public string? Usuario { get; set; }
    public DateTime CreadoEn { get; set; }
}
