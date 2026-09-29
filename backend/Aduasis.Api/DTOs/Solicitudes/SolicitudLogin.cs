namespace Aduasis.Api.DTOs.Solicitudes;

/// <summary>
/// Datos requeridos para iniciar sesión.
/// El campo 'credencial' acepta correo o nombre de usuario.
/// </summary>
public class SolicitudLogin
{
    /// <summary>Puede ser correo electrónico o nombre de usuario.</summary>
    public string Credencial { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
}
