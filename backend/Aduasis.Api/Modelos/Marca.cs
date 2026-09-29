namespace Aduasis.Api.Modelos;

/// <summary>
/// Marca o fabricante de un activo tecnológico.
/// Ejemplos: DELL, HP, CISCO, LENOVO, EPSON.
/// </summary>
public class Marca
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

    // Navegación
    public ICollection<ModeloActivo> Modelos { get; set; } = [];
    public ICollection<Activo> Activos { get; set; } = [];
}
