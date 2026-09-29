namespace Aduasis.Api.Modelos;

/// <summary>
/// Estado operativo de un activo tecnológico.
/// Ejemplos: Operativo, En reparación, Dado de baja, En bodega, En préstamo.
/// </summary>
public class EstadoActivo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public bool Activo { get; set; } = true;
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

    // Navegación
    public ICollection<Activo> Activos { get; set; } = [];
}
