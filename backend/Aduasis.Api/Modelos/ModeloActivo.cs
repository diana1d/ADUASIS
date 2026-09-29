namespace Aduasis.Api.Modelos;

/// <summary>
/// Modelo específico de un activo. Siempre pertenece a una marca.
/// Ejemplo: OPTIPLEX 7070 pertenece a DELL, LASERJET ENTERPRISE pertenece a HP.
/// La combinación (marca_id, nombre) es única.
/// </summary>
public class ModeloActivo
{
    public int Id { get; set; }
    public int MarcaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

    // Navegación
    public Marca Marca { get; set; } = null!;
    public ICollection<Activo> Activos { get; set; } = [];
}
