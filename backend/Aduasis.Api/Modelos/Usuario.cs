namespace Aduasis.Api.Modelos;

/// <summary>
/// Representa un usuario del sistema ADUASIS.
/// El rol determina qué interfaz y módulos puede ver el usuario.
/// Las acciones permitidas se controlan en el backend por rol.
/// </summary>
public class Usuario
{
    public int Id { get; set; }
    public int RolId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string NombreUsuario { get; set; } = string.Empty;

    /// <summary>
    /// Hash BCrypt de la contraseña. Nunca se almacena en texto plano.
    /// </summary>
    public string HashContrasena { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;
    public DateTime? UltimoAcceso { get; set; }
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
    public DateTime ActualizadoEn { get; set; } = DateTime.UtcNow;

    // Navegación
    public Rol Rol { get; set; } = null!;
    public ICollection<TokenRefresco> TokensRefresco { get; set; } = [];
}
