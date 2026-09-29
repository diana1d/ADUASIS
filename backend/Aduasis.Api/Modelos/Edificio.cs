namespace Aduasis.Api.Modelos;

/// <summary>
/// Edificio físico de la institución.
/// Contiene pisos que a su vez contienen áreas y espacios.
/// </summary>
public class Edificio
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public bool Activo { get; set; } = true;
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

    // Navegación
    public ICollection<Piso> Pisos { get; set; } = [];
}
