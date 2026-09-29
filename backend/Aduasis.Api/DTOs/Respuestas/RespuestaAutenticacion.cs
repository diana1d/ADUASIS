namespace Aduasis.Api.DTOs.Respuestas;

/// <summary>
/// Respuesta del servidor tras un login o refresco exitoso.
/// Contiene el access token JWT y el refresh token.
/// </summary>
public class RespuestaAutenticacion
{
    public string AccessToken { get; set; } = string.Empty;
    public string TokenRefresco { get; set; } = string.Empty;
    public DateTime ExpiraEn { get; set; }
    public UsuarioAutenticado Usuario { get; set; } = null!;
}

/// <summary>
/// Información básica del usuario incluida en la respuesta de autenticación.
/// Solo los datos que el frontend necesita para construir la interfaz.
/// NO incluye el hash de contraseña ni datos sensibles.
/// </summary>
public class UsuarioAutenticado
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string NombreUsuario { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
}
