namespace Aduasis.Api.Modelos;

/// <summary>
/// Token de refresco para renovar el access token JWT sin
/// volver a pedir credenciales. Permite revocar sesiones específicas.
/// Si el usuario se elimina, sus tokens se eliminan en cascada.
/// </summary>
public class TokenRefresco
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiraEn { get; set; }
    public bool Usado { get; set; } = false;
    public bool Revocado { get; set; } = false;
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// IP desde donde se generó el token. Útil para auditoría.
    /// </summary>
    public string? IpOrigen { get; set; }

    // Navegación
    public Usuario Usuario { get; set; } = null!;
}
