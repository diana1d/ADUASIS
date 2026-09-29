namespace Aduasis.Api.Modelos;

/// <summary>
/// Activo tecnológico registrado en el inventario de la institución.
///
/// Identificación basada en las etiquetas físicas de la Aduana Nacional:
/// - codigo_activo: código numérico de la etiqueta principal (ej: 100202130)
/// - codigo_qr:     código de la etiqueta QR secundaria (ej: 1103151400153)
/// </summary>
public class Activo
{
    public int Id { get; set; }

    // ── Identificación ──────────────────────────────────
    /// <summary>Código de activo de la etiqueta principal. Ej: 100202130</summary>
    public string CodigoActivo { get; set; } = string.Empty;

    /// <summary>Código de la etiqueta QR secundaria. Opcional.</summary>
    public string? CodigoQr { get; set; }

    // ── Descripción ─────────────────────────────────────
    /// <summary>Nombre descriptivo. Ej: "Computadora Diana López"</summary>
    public string NombreDescriptivo { get; set; } = string.Empty;

    /// <summary>
    /// Especificaciones técnicas en texto libre.
    /// Ej: "Intel Core i7, 8GB RAM, 1TB DD, color negro, incluye teclado y mouse"
    /// </summary>
    public string? Especificaciones { get; set; }

    public string? Color { get; set; }

    // ── Clasificación ────────────────────────────────────
    public int TipoActivoId { get; set; }
    public int MarcaId { get; set; }
    public int? ModeloActivoId { get; set; }

    // ── Estado ───────────────────────────────────────────
    public int EstadoActivoId { get; set; }

    // ── Ubicación ────────────────────────────────────────
    public int? EspacioId { get; set; }
    public int? AreaId { get; set; }

    // ── Asignación ───────────────────────────────────────
    public int? UsuarioAsignadoId { get; set; }

    // ── Red ──────────────────────────────────────────────
    public string? DireccionIp { get; set; }

    // ── Administrativo ───────────────────────────────────
    public DateOnly? FechaAdquisicion { get; set; }
    public string? Observaciones { get; set; }

    // ── Auditoría ────────────────────────────────────────
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
    public DateTime ActualizadoEn { get; set; } = DateTime.UtcNow;
    public int? CreadoPorId { get; set; }

    // ── Navegación ───────────────────────────────────────
    public TipoActivo TipoActivo { get; set; } = null!;
    public Marca Marca { get; set; } = null!;
    public ModeloActivo? ModeloActivo { get; set; }
    public EstadoActivo EstadoActivo { get; set; } = null!;
    public Espacio? Espacio { get; set; }
    public Area? Area { get; set; }
    public Usuario? UsuarioAsignado { get; set; }
    public Usuario? CreadoPor { get; set; }
}
