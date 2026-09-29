namespace Aduasis.Api.Modelos;

/// <summary>
/// Categoría general de un activo tecnológico.
/// Ejemplos: EQUIPO DE COMPUTACION, IMPRESORA, SWITCH, ROUTER, UPS.
/// </summary>
public class TipoActivo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public bool Activo { get; set; } = true;
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

    // Navegación
    public ICollection<Activo> Activos { get; set; } = [];
}
