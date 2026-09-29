namespace Aduasis.Api.DTOs.Solicitudes;

/// <summary>
/// Datos requeridos para renovar el access token usando el refresh token.
/// </summary>
public class SolicitudRefrescarToken
{
    public string TokenRefresco { get; set; } = string.Empty;
}
