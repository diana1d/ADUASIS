namespace Aduasis.Api.Modelos;

/// <summary>
/// Espacio físico (sala, oficina) dentro de un piso.
/// Un espacio pertenece al piso independientemente del área funcional.
/// Ejemplo: "Oficina 201", "Sala de servidores", "Recepción".
/// </summary>
public class Espacio
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
