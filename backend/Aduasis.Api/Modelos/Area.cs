namespace Aduasis.Api.Modelos;

/// <summary>
/// Área funcional/departamento dentro de un piso.
/// Ejemplo: Sistemas, Contabilidad, Recursos Humanos.
/// Un área pertenece a un piso específico.
/// </summary>
public class Area
{
    public int Id { get; set; }
    public int PisoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public bool Activo { get; set; } = true;
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

    // Navegación
    public Piso Piso { get; set; } = null!;
    public ICollection<Activo> Activos { get; set; } = [];
}
