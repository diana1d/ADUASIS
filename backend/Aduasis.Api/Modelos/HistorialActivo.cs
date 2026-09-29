namespace Aduasis.Api.Modelos;

/// <summary>
/// Registro de auditoría de cambios importantes sobre un activo.
///
/// Se genera automáticamente cuando el sistema detecta cambios relevantes
/// (estado, ubicación, asignación) y también permite notas manuales del usuario.
///
/// Tipos de evento automáticos: CREACION, CAMBIO_ESTADO, CAMBIO_UBICACION,
/// CAMBIO_ASIGNACION, ACTUALIZACION, BAJA.
/// Tipo de evento manual: NOTA.
/// </summary>
public class HistorialActivo
{
    public int Id { get; set; }
    public int ActivoId { get; set; }
    public int? UsuarioId { get; set; }

    /// <summary>
    /// Tipo de evento. Valores controlados desde la clase TipoEventoHistorial.
    /// </summary>
    public string TipoEvento { get; set; } = string.Empty;

    /// <summary>Descripción legible del cambio ocurrido.</summary>
    public string Descripcion { get; set; } = string.Empty;

    /// <summary>Valor antes del cambio. Ej: "Operativo"</summary>
    public string? ValorAnterior { get; set; }

    /// <summary>Valor después del cambio. Ej: "En reparación"</summary>
    public string? ValorNuevo { get; set; }

    /// <summary>
    /// True si fue ingresada manualmente por el usuario.
    /// False si fue generada automáticamente por el sistema.
    /// </summary>
    public bool EsNotaManual { get; set; } = false;

    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

    // Navegación
    public Activo Activo { get; set; } = null!;
    public Usuario? Usuario { get; set; }
}
