namespace Aduasis.Api.Modelos;

/// <summary>
/// Registro de asignación de un activo a un usuario.
///
/// La asignación activa se identifica por fecha_fin = NULL.
/// Cuando se reasigna o se da de baja, se cierra poniendo fecha_fin = NOW().
/// Un usuario_id = NULL representa un activo sin asignar (en bodega, etc.).
/// </summary>
public class Asignacion
{
    public int Id { get; set; }
    public int ActivoId { get; set; }
    public int? UsuarioId { get; set; }

    public DateTime FechaInicio { get; set; } = DateTime.UtcNow;

    /// <summary>NULL indica que la asignación está actualmente activa.</summary>
    public DateTime? FechaFin { get; set; }

    public string? Observaciones { get; set; }
    public int? RegistradoPorId { get; set; }
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

    // Navegación
    public Activo Activo { get; set; } = null!;
    public Usuario? Usuario { get; set; }
    public Usuario? RegistradoPor { get; set; }
}
