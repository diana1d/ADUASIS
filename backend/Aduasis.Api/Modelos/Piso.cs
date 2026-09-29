namespace Aduasis.Api.Modelos;

/// <summary>
/// Planta o nivel dentro de un edificio.
/// El campo 'orden' permite ordenarlos correctamente: PB=0, P1=1, Exterior=99.
/// </summary>
public class Piso
{
    public int Id { get; set; }
    public int EdificioId { get; set; }
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Orden visual: Planta Baja=0, Piso 1=1, Exterior=99, etc.
    /// </summary>
    public int Orden { get; set; } = 0;

    public bool Activo { get; set; } = true;
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

    // Navegación
    public Edificio Edificio { get; set; } = null!;
    public ICollection<Area> Areas { get; set; } = [];
    public ICollection<Espacio> Espacios { get; set; } = [];
}
