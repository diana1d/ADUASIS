namespace Aduasis.Api.Modelos;

/// <summary>
/// Representa un rol del sistema que determina qué interfaz
/// y acciones puede realizar un usuario autenticado.
/// Roles previstos: Administrador, Funcionario, Pasante.
/// </summary>
public class Rol
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public bool Activo { get; set; } = true;
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

    // Navegación
    public ICollection<Usuario> Usuarios { get; set; } = [];
}
