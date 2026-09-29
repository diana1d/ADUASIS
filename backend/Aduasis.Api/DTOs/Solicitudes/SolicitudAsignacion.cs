namespace Aduasis.Api.DTOs.Solicitudes;

/// <summary>
/// Datos para asignar un activo a un usuario.
/// </summary>
public class SolicitudAsignacion
{
    /// <summary>
    /// Id del usuario al que se asigna el activo.
    /// Si es null, se desasigna el activo (queda sin usuario asignado).
    /// </summary>
    public int UsuarioId { get; set; }
    public string? Observaciones { get; set; }
}
